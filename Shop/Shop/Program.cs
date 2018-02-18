using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;   
using System.Collections;
using System.Collections.Generic;

namespace Shop
{
	class Program
	{
		static void Main(string[] args)
		{
			File.Copy(@"constantproducts.txt","products.txt",true);
			Console.WriteLine("Введите ваше имя:");
			string customersname = Console.ReadLine();
			Console.Clear();
			Console.WriteLine("Отлично,теперь введите суммму на которую вы бы хотели пополнить свой счет:");
			int customersmoney = int.Parse(Console.ReadLine());
			Console.Clear();
			Console.WriteLine("ИНСТРУКЦИЯ:НАЖИМАЙТЕ 'ENTER' ДЛЯ ДОБАВЛЕНИЯ ТОВАРА В КОРЗИНУ,'B' ДЛЯ ПОКУПКИ ВСЕХ ТОВАРОВ В КОРЗИНЕ,'A' ДЛЯ ДОБАВЛЕНИЯ ТОВАРА");
			Console.ReadKey();
			Console.Clear();
			Shop shop = new Shop();
			shop.ReadProducts();
			int cursor = 0,change = 0;
			int n = shop.products.Count;
			List<string> chek = new  List<string>();
			shop.ShowAllProducts(cursor);
			while(true){
				ConsoleKeyInfo kI = Console.ReadKey();
				if(kI.Key == ConsoleKey.DownArrow){
					cursor++;
					if(cursor == n){
						cursor = 0;
					}
				}
				if(kI.Key == ConsoleKey.UpArrow){
					cursor--;
					if(cursor == -1){
						cursor = n - 1;
					}
				}
				if(kI.Key == ConsoleKey.Enter){
					shop.AddtoBucket(shop.products[cursor]);
				}
				if(kI.Key == ConsoleKey.B){
					change = shop.Buy(customersmoney);
					chek = shop.Pokupki();
					break;
				}
				if(kI.Key == ConsoleKey.A){
					shop.AddProduct();
				}
				shop.ShowAllProducts(cursor);
			}
			Console.BackgroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.WriteLine("Спасибо за покупку "+customersname+",приходите еще.Ваша сдача:"+change.ToString());
			Console.WriteLine("Ваш чек:");
			foreach(string s in chek){
				Console.WriteLine(s);
			}
		}
	}
}
