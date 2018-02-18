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
            Fruit fruit = new Fruit();
            int level = 1;
            int cnt = 0;
            int score = 0;
            int kek = 0;
            while(true){
                if(cnt == 3){
                    level++;
                    cnt=0;
                }
                Wall wall = new Wall (level);
                ConsoleKeyInfo keyInfo = Console.ReadKey(); 
                if(keyInfo.Key == ConsoleKey.UpArrow){
                    if(kek == 1) continue;
                    else snake.Move(0,-1);
                    kek = 2;
                }
                if(keyInfo.Key == ConsoleKey.DownArrow){
                    if(kek == 2) continue;
                    else snake.Move(0,1);
                    kek = 1;
                }
                if(keyInfo.Key == ConsoleKey.LeftArrow){
                    if(kek == 3) continue;
                    else snake.Move(-1,0);
                    kek = 4;
                }
                if(keyInfo.Key == ConsoleKey.RightArrow){
                    if(kek == 4) continue;
                    else snake.Move(1,0);
                    kek = 3;
                }
                if(keyInfo.Key == ConsoleKey.R){
                    snake = new Snake();
                }
                if(keyInfo.Key == ConsoleKey.S){
                    snake.Serialization();
                    wall.Serialization();
                    fruit.Serialization();
                }
                while(snake.Inthesnake(fruit.coordinates.x,fruit.coordinates.y) || snake.Inthewall(fruit.coordinates.x, fruit.coordinates.y, wall)){
                    fruit.FoodMaker();
                }
                if(snake.Bump() || snake.Collide(wall)){
                    Console.Clear();
                    Console.SetCursorPosition(50,50);
                    Console.WriteLine("GAME OVER");
                    Console.ReadKey();
                    snake = new Snake();
                    level = 1;
                    score  = 0;
                }
                
                if(snake.Eaten(fruit) == true){
                    cnt++;
                    score++;   
                }
                Console.Clear();
                snake.Draw();
                fruit.DrawFood();
                wall.Draw();
                Console.SetCursorPosition(150,200);
                Console.Write("Score:"+score.ToString());
                Console.Write("Level:"+level);
            } 
        }
    }
}









