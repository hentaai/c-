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
            int level = 1;
            int cnt = 0;
            int score = 0;
            Console.Clear();
            fruit.FoodMaker(coordinatess);
            fruit.DrawFood(coordinatess.x,coordinatess.y);
            while(true){
                if(cnt == 3){
                    level++;
                    cnt=0;
                }
                Wall wall = new Wall (level);
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
                if(snake.Inthesnake(coordinatess.x,coordinatess.y) || snake.Inthewall(coordinatess.x, coordinatess.y, wall)){
                    fruit.FoodMaker(coordinatess);
                }
                if(snake.Bump() || snake.Collide(wall)){
                    Console.Clear();
                    Console.SetCursorPosition(50,50);
                    Console.WriteLine("GAME OVER");
                    Console.ReadKey();
                    snake = new Snake();
                    level = 1;
                }
                if(snake.Eaten(coordinatess.x,coordinatess.y) == true){
                    fruit.FoodMaker(coordinatess);
                    Console.Clear();
                    snake.Draw();
                    fruit.DrawFood(coordinatess.x,coordinatess.y);
                    wall.Draw();
                    cnt++;
                    score++;
                    Console.SetCursorPosition(150,200);
                    Console.Write("Score:"+score.ToString());
                    Console.Write("Level:"+level);
                }
                else{
                    Console.Clear();
                    snake.Draw();
                    fruit.DrawFood(coordinatess.x,coordinatess.y);
                    wall.Draw();
                    Console.SetCursorPosition(150,200);
                    Console.Write("Score:"+score.ToString());
                    Console.Write("Level:"+level);
                }
            } 
        }
    }
}









