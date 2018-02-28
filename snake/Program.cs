using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;   
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace snake
{
    public class Program
    {       
        static Snake snake = new Snake();
        static Fruit fruit = new Fruit();
        static int level = 1;
        static Wall wall = new Wall (level); 
        static int cnt = 0;
        static int cntsave = 0;
        static int score = 0;
        static int scoresave = 0;
        static int direction = 1;
        static bool gameOver = false;
        static int speed = 200;    

        static void Game(){
            snake.body.Add(new Point(20,20));
            while(!gameOver){
                if(cnt == 3){
                    level++;
                    cnt=0;
                    wall = new Wall(level);
                }

                if(direction == 1 ){
                    snake.Move(0,-1);
                }
                if(direction == 2){
                    snake.Move(0,1);
                }
                if(direction == 3){
                    snake.Move(1,0);
                }
                if(direction == 4){
                    snake.Move(-1,0);
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
                    snake.body.Add(new Point(20,20));
                    level = 1;
                    score  = 0;
                    speed = 500;
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

                if(score%3 == 0 && score != 0 ){
                    speed = Math.Max(speed - 1,1);
                }

                Thread.Sleep(speed);
            } 
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Thread thread = new Thread(Game);
            thread.Start();

            while(!gameOver){
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if(keyInfo.Key == ConsoleKey.UpArrow){
                    direction = 1;
                }
                if(keyInfo.Key == ConsoleKey.DownArrow){
                    direction = 2;
                }
                if(keyInfo.Key == ConsoleKey.RightArrow){
                    direction = 3;
                }
                if(keyInfo.Key == ConsoleKey.LeftArrow){
                    direction = 4;
                }
                if(keyInfo.Key == ConsoleKey.Escape){
                    gameOver = true;
                }
                if(keyInfo.Key == ConsoleKey.R){
                    snake = new Snake();
                }
                if(keyInfo.Key == ConsoleKey.S){
                    snake.Serialization();
                    wall.Serialization();
                    fruit.Serialization();
                    scoresave = score;
                    cntsave = cnt;
                }
                if(keyInfo.Key == ConsoleKey.L){
                    snake = snake.Deserialization();
                    wall = wall.Deserialization();
                    fruit = fruit.Deserialization();
                    score = scoresave;
                    cnt = cntsave ;
                }
            }
        }
    }
}









