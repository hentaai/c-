using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;   
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace snake
{   
	[Serializable, XmlRoot("Fruit")]
	public class Fruit
	{
		public Random rnd = new Random ();
		public Point coordinates = new Point (0,0);

		public Fruit(){
			coordinates.x = rnd.Next(2,Console.WindowWidth);
			coordinates.y = rnd.Next(2,Console.WindowHeight);
		}
		public void FoodMaker(){
			coordinates.x = rnd.Next(2,Console.WindowWidth);
			coordinates.y = rnd.Next(2,Console.WindowHeight);
		}
		public void DrawFood(){
			Console.SetCursorPosition(coordinates.x,coordinates.y);
			Console.Write("*");
			Console.CursorVisible = false;
		}
		public void Serialization(){
			XmlSerializer xs = new XmlSerializer(typeof(Fruit));
			FileStream fs = new FileStream("data1.xml",FileMode.Create);
			xs.Serialize(fs, this);
			fs.Close();
		}
		public Fruit Deserialization(){
			XmlSerializer xs = new XmlSerializer(typeof(Fruit));
			FileStream fs = new FileStream("data1.xml",FileMode.OpenOrCreate);
			Fruit fruit = xs.Deserialize(fs) as Fruit;
			fs.Close();
			return fruit;
		}
	}

}