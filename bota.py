import csv
import urllib.request
import datetime
from bs4 import BeautifulSoup

import requests

from time import sleep

#url = "https://api.telegram.org/bot500967450:AAEnql1rqEnw7MtpvujpR07bDvxwAVfl4_g/"

BASE_URL = 'https://krisha.kz/prodazha/kvartiry/'

BASE_URL1 = 'https://krisha.kz/arenda/kvartiry/?das[live.rooms][]=1&das[live.rooms][]=2'

url = BASE_URL

def get_html(url):
    response = urllib.request.urlopen(url)
    return response.read()

def parse(html):
    soup = BeautifulSoup(html,"lxml")
    titles = []
    prices = []
    towns = []
    Zhks = []
    urls = []
    links = []
    hozyain = []
    table = soup.find('section', class_='a-list a-search-list a-list-with-favs')
    usertitle = soup.find('div', class_='user-title user-title-not-pro')
    k=0
    for row in table.find_all('div',class_='a-description'):

        imena = row.find_all('div', class_='a-title')

        prices = row.find_all('span', class_='a-price-value')

        towns = row.find_all('div', class_='a-subtitle')

        Zhks = row.find_all('div', class_='a-text')

        urls = row.find_all('a', class_='link')

        
        i=0

        for town in towns:
            j=0

            t = ''

            town = towns[i].text

            while town[j]!=',':
                t = t + town[j]
                
                j = j+1

            towns[i] = t

        i=0

        for Zhk in Zhks:
            j=0

            t = ''

            s = Zhks[i].text

            Zhk = Zhks[i].text

            if Zhk.startswith('\n                жил. комплекс')==True:

                while Zhk[j]!=',':
                    t = t + Zhk[j]
                    j = j+1

                Zhks[i] = t
            
            else:

                Zhks[i] = 'нет'

        i=0


        first = prices[0].text.split()[0]
        second = first + " " + prices[0].text.split()[1]
        third = second + " " + prices[0].text.split()[2]
       	zh = Zhks[0].replace("\n                жил. комплекс ","")
        titles.append({
            'title': imena[0].a.text,
            'город': towns[0],
            'цена': third,  
            'Жилой Комплекс': zh,
            'Ссылка' :urls[0].get('href')
        })
        k = k + 1
    
    return titles


'''def text_of_mess(titles):
    text_of_message = []

    for title in titles:
        text_of_message.append((title['title'], title['город'], title['Жилой Комплекс'], title['цена'], title['Ссылка']))

    text = str(text_of_message)    
    
    return text
'''

def gorod(string, titles):

	goroda = []

	for title in titles:
		title['title'] = title['title'] + ":"
		title['город'] = title['город'] + ":"
		title['Жилой Комплекс'] = title['Жилой Комплекс'] + ":"
		title['Ссылка'] = "krisha.kz"+title['Ссылка']
	string = string + ":"
	string = string.replace("/","")
	for title in titles:
		if string.lower() == title['город'].lower():
			goroda.append({
				'title': title['title'], 
				'город': title['город'],
				'цена': title['цена'],
				'Жилой Комплекс': title['Жилой Комплекс'],
				'Ссылка': title['Ссылка']
			})

	return goroda


def jcomplex(string, goroda):

	jcomplex = []
	string = string + ":"
	string = string.replace("/","")
	for title in goroda:
		if string.lower() == title['Жилой Комплекс'].lower():
			jcomplex.append((title['title'], title['город'], title['Жилой Комплекс'], title['цена'], title['Ссылка']))

	if len(jcomplex)==0:
		jcomplex.append("По вашему запросу ничего не найдено")

	return str(jcomplex)

def beautify_the_text(string):

	count = 1
	const = 1
	for t in string:
		if t == "(":
			i = str(count)+".  "
			string = string.replace("(", i, const)
			count = count + 1

	text = string.replace(")","\n")

	text = text.replace("'","")

	text = text.replace(",","")

	text = text.replace(":",",")

	text = text.replace("https,//","")

	text = text.replace("[","")

	text = text.replace("]","")
	return text


class BotHandler:
	
	def __init__(self,token):
		self.token = token
		self.api_url = "https://api.telegram.org/bot{}/".format(token)

	def get_updates(self, offset=None, timeout=30):
		method = 'getUpdates'
		params = {'timeout': timeout, 'offset': offset}
		resp = requests.get(self.api_url + method, params)
		result_json = resp.json()['result']
		return result_json

	def send_message(self, chat_id, text):
		params = {'chat_id': chat_id, 'text': text}
		method = 'sendMessage'
		resp = requests.post(self.api_url + method, params)
		return resp

	def get_last_update(self):
		get_result = self.get_updates()

		last_update = get_result[len(get_result)-1]

		return last_update


token = '580949049:AAEP61CW-08v9TnlEmCfdl9HWkb0aQ2lmkY'
krisha_bot = BotHandler(token)  

command1 = '/start'
command2 = '/help' 
command3 = '/аренда'
command4 = '/продажа'
command5 = '/re'
def main():  
	new_offset = None
	while True:
		krisha_bot.get_updates(new_offset)
		last_update = krisha_bot.get_last_update()
		last_update_id = last_update['update_id']
		last_chat_text = last_update['message']['text']
		last_chat_id = last_update['message']['chat']['id']
		last_chat_name = last_update['message']['chat']['first_name']
		
		if last_chat_text.lower()==command1:
			krisha_bot.send_message(last_chat_id, "Я бот,я буду показывать вам объявления с сайта krisha.kz по вашему запросу,для полной информаций напишите /help")

		elif last_chat_text.lower()==command2:
			krisha_bot.send_message(last_chat_id, "Все ваши запросы пишите с обратным слэшом (/) и на русском языке, все название что вы вводите должно совпадать с данными на крыше,если ошибочно ввели данные то напишите /re и настраивайте все заново")

		elif last_chat_text.lower()==command3:
			krisha_bot.send_message(last_chat_id, "Отлично, теперь введите пожалуйста название города в этом формате '/город' ")
			
			new_offset=last_update_id+1
			krisha_bot.get_updates(new_offset)
			last_update = krisha_bot.get_last_update()
			last_update_id = last_update['update_id']
			last_chat_text = last_update['message']['text']
			last_chat_id = last_update['message']['chat']['id']
			last_chat_name = last_update['message']['chat']['first_name']
			
			if last_chat_text.lower()==command5:
				main()

			qala=last_chat_text.lower()
			krisha_bot.send_message(last_chat_id, "Отлично, теперь введите пожалуйста название жилого комплекса в этом формате '/ЖК',а если его нет то введите '/нет'")
			
			new_offset=last_update_id+1
			krisha_bot.get_updates(new_offset)
			last_update = krisha_bot.get_last_update()
			last_update_id = last_update['update_id']
			last_chat_text = last_update['message']['text']
			last_chat_id = last_update['message']['chat']['id']
			last_chat_name = last_update['message']['chat']['first_name']

			if last_chat_text.lower()==command5:
				main()
			
			j = last_chat_text.lower()
			krisha_bot.send_message(last_chat_id, "Отлично, теперь введите цену в этом формате '/мин.цена_макс.цена' ")
					
			new_offset=last_update_id+1
			krisha_bot.get_updates(new_offset)
			last_update = krisha_bot.get_last_update()
			last_update_id = last_update['update_id']
			last_chat_text = last_update['message']['text']
			last_chat_id = last_update['message']['chat']['id']
			last_chat_name = last_update['message']['chat']['first_name']

			if last_chat_text.lower()==command5:
				main()

			c = last_chat_text.split("_")[0]
			c = c.replace("/","")
			d = last_chat_text.split("_")[1]
			l = BASE_URL1 + "&das[price][from]="+c+"&das[price][to]="+d

			krisha_bot.send_message(last_chat_id, "Отлично, теперь введите пожалуйста '/месяц' или '/сутки' или '/квартал' или '/час'в зависимости от ваших пожеланий")
		
			new_offset=last_update_id+1
			krisha_bot.get_updates(new_offset)
			last_update = krisha_bot.get_last_update()
			last_update_id = last_update['update_id']
			last_chat_text = last_update['message']['text']
			last_chat_id = last_update['message']['chat']['id']		
			last_chat_name = last_update['message']['chat']['first_name']

			if last_chat_text.lower()==command5:
				main()

			if last_chat_text.lower()=='/месяц':
				li = l + '&das[rent.period]=2' 
			elif last_chat_text.lower()=='/сутки':
				li = l + '&das[rent.period]=1'
			elif last_chat_text.lower()=='/квартал':
				li = l + '&das[rent.period]=3'
			elif last_chat_text.lower()=='/час':
				li = l + '&das[rent.period]=4'
			krisha_bot.send_message(last_chat_id, "Отлично, теперь введите пожалуйста '/хозяйн' или '/специалисты' или '/все' в зависимости от ваших пожеланий")

			new_offset=last_update_id+1
			krisha_bot.get_updates(new_offset)
			last_update = krisha_bot.get_last_update()
			last_update_id = last_update['update_id']
			last_chat_text = last_update['message']['text']
			last_chat_id = last_update['message']['chat']['id']
			last_chat_name = last_update['message']['chat']['first_name']

			if last_chat_text.lower()==command5:
				main()

			if last_chat_text.lower()=='/хозяйн':
				lin = li + '&das[who]=1'
			if last_chat_text.lower()=='/специалисты':
				lin = li + '&das[checked]=1'
			if last_chat_text.lower()=='/все':
				lin = li 

			whole = []	
								
			for page in range(1, 15):
				whole.extend(parse(get_html(lin+"&page=%d" % page)))

			krisha_bot.send_message(last_chat_id, beautify_the_text(jcomplex(j,gorod(qala, whole))))
	
		elif last_chat_text.lower()==command4:
			krisha_bot.send_message(last_chat_id, "Отлично, теперь введите пожалуйста название города в этом формате '/город' ")
			
			new_offset=last_update_id+1
			krisha_bot.get_updates(new_offset)
			last_update = krisha_bot.get_last_update()
			last_update_id = last_update['update_id']
			last_chat_text = last_update['message']['text']
			last_chat_id = last_update['message']['chat']['id']
			last_chat_name = last_update['message']['chat']['first_name']
			
			if last_chat_text.lower()==command5:
				main()

			qala=last_chat_text.lower()
			krisha_bot.send_message(last_chat_id, "Отлично, теперь введите пожалуйста название жилого комплекса в этом формате '/ЖК',а если его нет то введите '/нет'")
			
			new_offset=last_update_id+1
			krisha_bot.get_updates(new_offset)
			last_update = krisha_bot.get_last_update()
			last_update_id = last_update['update_id']
			last_chat_text = last_update['message']['text']
			last_chat_id = last_update['message']['chat']['id']
			last_chat_name = last_update['message']['chat']['first_name']

			if last_chat_text.lower()==command5:
				main()
			
			j = last_chat_text.lower()
			krisha_bot.send_message(last_chat_id, "Отлично, теперь введите цену в этом формате '/мин.цена_макс.цена' ")
					
			new_offset=last_update_id+1
			krisha_bot.get_updates(new_offset)
			last_update = krisha_bot.get_last_update()
			last_update_id = last_update['update_id']
			last_chat_text = last_update['message']['text']
			last_chat_id = last_update['message']['chat']['id']
			last_chat_name = last_update['message']['chat']['first_name']

			if last_chat_text.lower()==command5:
				main()

			c = last_chat_text.split("_")[0]
			c = c.replace("/","")
			d = last_chat_text.split("_")[1]
			l = BASE_URL + "?das[price][from]="+c+"&das[price][to]="+d

			krisha_bot.send_message(last_chat_id, "Отлично, теперь введите пожалуйста '/хозяйн' или '/специалисты' или '/все' в зависимости от ваших пожеланий")

			new_offset=last_update_id+1
			krisha_bot.get_updates(new_offset)
			last_update = krisha_bot.get_last_update()
			last_update_id = last_update['update_id']
			last_chat_text = last_update['message']['text']
			last_chat_id = last_update['message']['chat']['id']
			last_chat_name = last_update['message']['chat']['first_name']

			if last_chat_text.lower()==command5:
				main()

			if last_chat_text.lower()=='/хозяйн':
				lin = l + '&das[who]=1'
			if last_chat_text.lower()=='/специалисты':
				lin = l + '&das[checked]=1'
			if last_chat_text.lower()=='/все':
				lin = l

			whole = []	
								
			for page in range(1, 15):
				whole.extend(parse(get_html(lin+"&page=%d" % page)))

			krisha_bot.send_message(last_chat_id, beautify_the_text(jcomplex(j,gorod(qala, whole))))

		new_offset = last_update_id + 1

if __name__ == '__main__':  
	try:
		main()
	except KelyboardInterrupt:
		exit()
    
