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
    public class Fruits
    {
        public Random rnd = new Random ();
        public Point coordinates = new Point (0,0);
        public Fruits(){ }
        public void FoodMaker(Point coo){
            coo.x = rnd.Next(2,Console.WindowWidth);
            coo.y = rnd.Next(2,Console.WindowHeight);
        }
        public void DrawFood(int x,int y){
            Console.SetCursorPosition(x,y);
            Console.Write("*");
            Console.CursorVisible = false;
        }
    }

}