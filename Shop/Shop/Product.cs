using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;   
using System.Collections;
using System.Collections.Generic;

namespace Shop
{
	public class Product
	{
		public string name;
		public string price;
		public Product(){ }
		public Product(string name,string price){
			this.name = name;
			this.price = price;
		}
	}
}