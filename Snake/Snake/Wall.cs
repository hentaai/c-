using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace snake
{
	public class Wall{
		public List<Point> body;
		public string sign;
		public ConsoleColor color;

		public void ReadLevel(int level){
			StreamReader sr = new StreamReader(@"/home/yera/c-/snake/levels/level"+level+".txt");
			int n = int.Parse(sr.ReadLine());
			for(int i=0;i<n;i++){
				string s = sr.ReadLine();
				for(int j=0;j<s.Length;j++){
					if(s[j] == '*'){
						body.Add(new Point(j,i));
					}   
				}
			}
			sr.Close();
		}
		public Wall(){}
		public Wall(int level){
			body = new List<Point>();
			sign = "*";
			color = ConsoleColor.Magenta;
			ReadLevel(level);
		}
		public void Draw(){
			Console.ForegroundColor = color;
			foreach(Point p in body){
				Console.SetCursorPosition(p.x,p.y);
				Console.Write(sign);
			}
		}
		public void Serialization(){
			XmlSerializer xs = new XmlSerializer(typeof(Wall));
			FileStream fs = new FileStream("data2.xml",FileMode.Create);
			xs.Serialize(fs, this);
			fs.Close();
		}
		public Wall Deserialization(){
			XmlSerializer xs = new XmlSerializer(typeof(Wall));
			FileStream fs = new FileStream("data2.xml",FileMode.OpenOrCreate);
			Wall wall = xs.Deserialize(fs) as Wall;
			fs.Close();
			return wall;
		}
	}
}