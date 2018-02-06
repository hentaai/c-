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
    class Program
    {
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            Point coordinatess = new Point(0,0);
            Fruits fruit = new Fruits();
            Random rnd = new Random ();
            Console.Clear();
            fruit.FoodMaker(coordinatess);
            fruit.DrawFood(coordinatess.x,coordinatess.y);
            while(true){
                ConsoleKeyInfo keyInfo = Console.ReadKey(); 
                if(keyInfo.Key == ConsoleKey.UpArrow){
                    snake.Move(0,-1);
                }
                if(keyInfo.Key == ConsoleKey.DownArrow){
                    snake.Move(0,1);
                }
                if(keyInfo.Key == ConsoleKey.LeftArrow){
                    snake.Move(-1,0);
                }
                if(keyInfo.Key == ConsoleKey.RightArrow){
                    snake.Move(1,0);
                }
                if(keyInfo.Key == ConsoleKey.R){
                    snake = new Snake();
                }
                if(snake.Bump()){
                    Console.Clear();
                    Console.SetCursorPosition(5,5);
                    Console.WriteLine("GAME OVER");
                    Console.ReadKey();
                    snake = new Snake();
                }
                if(snake.Eaten(coordinatess.x,coordinatess.y) == true){
                    fruit.FoodMaker(coordinatess);
                    Console.Clear();
                    snake.Draw();
                    fruit.DrawFood(coordinatess.x,coordinatess.y);
                }
                else{
                    Console.Clear();
                    snake.Draw();
                    fruit.DrawFood(coordinatess.x,coordinatess.y);
                }
            } 
        }
    }
}









