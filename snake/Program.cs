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
        static Snake snake1 = new Snake();
        static Fruit fruit = new Fruit();
        static int level = 1;
        static Wall wall = new Wall (level); 
        static int cnt = 0;
        static int score = 0;
        static int direction = 1;
        static int direction1 = 1;
        static bool gameOver = false;
        static bool gameOver1 = false;
        static int speed = 110;
        static int check = 0;

        static void Game(){
            snake.body.Add(new Point(20,20));
            while(!gameOver){
                if(cnt == 3){
                    level++;
                    cnt=0;
                    wall = new Wall(level);
                }
                if(direction == 1){
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

                if(direction1 == 1){
                    snake1.Move(0,-1);
                }
                if(direction1 == 2){
                    snake1.Move(0,1);
                }
                if(direction1 == 3){
                    snake1.Move(1,0);
                }
                if(direction1 == 4){
                    snake1.Move(-1,0);
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
                    cnt = 0;
                    speed = 120;
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

                if(cnt%3 == 0 && cnt != 0 ){
                    speed = Math.Max(speed - 1,1);
                }

                Thread.Sleep(speed);
            } 
        }
         static void Game1(){
            snake1.body.Add(new Point(20,20));
            while(!gameOver1){
                if(direction1 == 1){
                    snake1.Move(0,-1);
                }
                if(direction1 == 2){
                    snake1.Move(0,1);
                }
                if(direction1 == 3){
                    snake1.Move(1,0);
                }
                if(direction1 == 4){
                    snake1.Move(-1,0);
                }
                
                snake1.Draw();
                Thread.Sleep(speed);
            } 
        }

        static void GameFilesS(){
            XmlSerializer xs = new XmlSerializer(typeof(int));
            FileStream fs = new FileStream("/home/yera/c-/snake/data3.xml",FileMode.Create);
            xs.Serialize(fs,speed);
            fs.Close();
            xs = new XmlSerializer(typeof(int));
            fs = new FileStream("/home/yera/c-/snake/data4.xml",FileMode.Create);
            xs.Serialize(fs,level);
            fs.Close();
            xs = new XmlSerializer(typeof(int));
            fs = new FileStream("/home/yera/c-/snake/data5.xml",FileMode.Create);
            xs.Serialize(fs,score);
            fs.Close();
            xs = new XmlSerializer(typeof(int));
            fs = new FileStream("/home/yera/c-/snake/data6.xml",FileMode.Create);
            xs.Serialize(fs,cnt);
            fs.Close();
        }
        static void GameFilesD(int speed,int level,int score,int cnt){
            XmlSerializer xs = new XmlSerializer(typeof(int));
            FileStream fs = new FileStream("/home/yera/c-/snake/data3.xml",FileMode.Open,FileAccess.ReadWrite);
            object obj = xs.Deserialize(fs);
            speed = (int)obj;
            fs.Close();
            xs = new XmlSerializer(typeof(int));
            fs = new FileStream("/home/yera/c-/snake/data4.xml",FileMode.Open,FileAccess.ReadWrite);
            object obj1 = xs.Deserialize(fs);
            level = (int)obj1;
            fs.Close();
            xs = new XmlSerializer(typeof(int));
            fs = new FileStream("/home/yera/c-/snake/data5.xml",FileMode.Open,FileAccess.ReadWrite);
            object obj2 = xs.Deserialize(fs);
            score = (int)obj2;
            fs.Close();
            xs = new XmlSerializer(typeof(int));
            fs = new FileStream("/home/yera/c-/snake/data6.xml",FileMode.Open,FileAccess.ReadWrite);
            object obj3 = xs.Deserialize(fs);
            cnt = (int)obj3;
            fs.Close();
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Thread thread = new Thread(Game);
            Thread thread1 = new Thread(Game1);
            thread.Start();
            thread1.Start();

            while(!gameOver){
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if(keyInfo.Key == ConsoleKey.UpArrow && check != 1){
                    direction = 1;
                    check = 2;
                }
                if(keyInfo.Key == ConsoleKey.DownArrow && check != 2){
                    direction = 2;
                    check = 1;
                }
                if(keyInfo.Key == ConsoleKey.RightArrow && check != 3){
                    direction = 3;
                    check = 4;
                }
                if(keyInfo.Key == ConsoleKey.LeftArrow && check != 4){
                    direction = 4;
                    check = 3;
                }
                if(keyInfo.Key == ConsoleKey.W && check != 1){
                    direction1 = 1;
                    check = 2;
                }
                if(keyInfo.Key == ConsoleKey.S && check != 2){
                    direction1 = 2;
                    check = 1;
                }
                if(keyInfo.Key == ConsoleKey.D && check != 3){
                    direction1 = 3;
                    check = 4;
                }
                if(keyInfo.Key == ConsoleKey.A && check != 4){
                    direction1 = 4;
                    check = 3;
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
                    GameFilesS();
                }
                if(keyInfo.Key == ConsoleKey.L){
                    snake = snake.Deserialization();
                    wall = wall.Deserialization();
                    fruit = fruit.Deserialization();
                    GameFilesD(speed,level,score,cnt);
                }
            }
        }
    }
}









