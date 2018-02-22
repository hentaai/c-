using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace snake
{

    public class Snake
    {
        public List<Point> body;
        public string sign;
        public ConsoleColor color = ConsoleColor.Yellow;        

        public Snake()
        {
            body.Add(new Point(20,20));
            body = new List<Point>();
            sign = "o";
        }

        public void Move(int dx, int dy){
            for(int i = body.Count - 1; i > 0; i--){
                body[i].x = body[i-1].x;
                body[i].y = body[i-1].y;
            }

            body[0].x += dx;
            body[0].y += dy;
            
            if(body[0].x < 1){
                body[0].x = Console.WindowWidth - 1;
            }
            if(body[0].x > Console.WindowWidth - 1){
                body[0].x = 1;
            }
            if(body[0].y < 1){
                body[0].y = Console.WindowHeight - 1;
            }
            if(body[0].y > Console.WindowHeight - 1){
                body[0].y = 1;
            }
        }
        public void Serialization(){
            XmlSerializer xs = new XmlSerializer(typeof(Snake));
            FileStream fs = new FileStream("data.xml",FileMode.Create,FileAccess.ReadWrite);
            xs.Serialize(fs, this);
            fs.Close();
        }
        public Snake Deserialization(){
            XmlSerializer xs = new XmlSerializer(typeof(Snake));
            FileStream fs = new FileStream("data.xml",FileMode.OpenOrCreate,FileAccess.ReadWrite);
            Snake snake = xs.Deserialize(fs) as Snake;
            fs.Close();
            return snake;
        }
        public bool Eaten(Fruit fruit){
            if(body[0].x == fruit.coordinates.x && body[0].y == fruit.coordinates.y){ 
                body.Add(new Point(0,0));
                fruit.FoodMaker();
                return true;
            }
            return false;
        }
        public bool Inthesnake(int co1,int co2){
            for(int i = 1; i<body.Count; i++){
                if(co1 == body[i].x && co2 == body[i].y){
                    return true;
                }
            }
            return false;
        }
        public bool Inthewall(int co1,int co2,Wall s){
            foreach(Point p in s.body){
                if(co1 == p.x && co2 == p.y){
                    return true;
                }
            }
            return false;
        }
        public void Draw(){
            int index = 0;
            foreach(Point p in body){
                if(index == 0){
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else{
                    Console.ForegroundColor = color;
                }
                if(p.x != 0 && p.y !=0){
                    Console.SetCursorPosition(p.x,p.y);
                    Console.Write(sign);
                    index++;
                }
            }   
        }
        public bool Collide(Wall s){
            foreach(Point p in s.body){
                if(p.x == body[0].x && p.y == body[0].y)
                return true;
            }
            return false;
        }
        public bool Bump(){
            for(int i = 1;i < body.Count;i++){
                if(body[0].x == body[i].x && body[0].y == body[i].y){
                    return true;
                }
            }
            return false;
        }

    }
}