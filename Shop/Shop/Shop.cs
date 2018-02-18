using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;   
using System.Collections;
using System.Collections.Generic;

namespace Shop
{
	public class Shop
	{
		public List<Product> products;
		public HashSet<Product> bucket;
		public Shop(){
			products = new List<Product>();
			bucket = new HashSet<Product>();
		}
		public void AddProduct(){
			Console.Clear();
			Console.WriteLine("Введите название товара:");
			string name = Console.ReadLine();
			Console.WriteLine("Введите цену товара в тенге:");
			string price = Console.ReadLine();
			StreamReader sr = new StreamReader(@"constantproducts.txt");
			int n = int.Parse(sr.ReadLine());
			string str = "";
			for(int i = 0 ;i < n; i++){
				str += "\n";
				str += sr.ReadLine();
			}
			sr.Close ();
			str = (n+1).ToString()+str+"\n"+name+":"+price;
			File.WriteAllText(@"constantproducts.txt",str);
			Product p = new Product(name,price);
			products.Add(p);
		}  
		public void AddtoBucket(Product p){
			bucket.Add(p);
			products.Remove(p);
		}
		public List<string> Pokupki(){
			List<string> pokupki = new List<string>();
			foreach(Product p in bucket){
				pokupki.Add(p.name+"  за  "+p.price);
			}
			return pokupki;
		}
		public int Buy(int customermoney){
			int wholeprice = 0;
			foreach(Product s in bucket){
				wholeprice += int.Parse(s.price);
			}
			int change = customermoney-wholeprice;
			return change;
		}
		public void ReadProducts(){
			products.Clear();
			StreamReader sr = new StreamReader(@"products.txt");
			int n = int.Parse(sr.ReadLine());
			string name = "";
			string price = "";
			for(int i = 0;i < n; i++){
				string s = sr.ReadLine();
				for(int j = 0;j < s.Length; j++){
					name = s.Split(':')[0];
					price = s.Split(':')[1];
				}
				products.Add(new Product(name,price));
			}
			sr.Close();
		}
		public void ShowAllProducts(int cursor){
			Console.BackgroundColor = ConsoleColor.Black;
			Console.Clear();
			Console.SetCursorPosition(0,0);
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine("Название продукта");
			Console.SetCursorPosition(30,0);
			Console.WriteLine("Цена продукта(тг)");
			Console.SetCursorPosition(60,0);
			Console.WriteLine("Корзина");
			int perehod = 1;
			for(int i = 0;i < products.Count; i++){
				Product product = products[i];
				if(i == cursor){
					Console.BackgroundColor = ConsoleColor.Gray;
				}
				else{
					Console.BackgroundColor = ConsoleColor.Black;
				}
				Console.ForegroundColor = ConsoleColor.Red;
				Console.SetCursorPosition(0,perehod*2);
				Console.WriteLine(product.name);
				Console.SetCursorPosition(30,perehod*2);
				Console.WriteLine(product.price);
				perehod++;
			}
			int l = 0;
			foreach(Product q in bucket){
				Console.SetCursorPosition(60,l+1);
				Console.WriteLine(q.name);
				l++;
			}
		}
	}
}