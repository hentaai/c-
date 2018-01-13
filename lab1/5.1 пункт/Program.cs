 т﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Rectangle{
        public int width;
        public int height;
        public int area;
        public int peri;
        public Rectangle(int a, int b){
            width = a;
            height = b;
            area = a*b;
            peri = 2*(a+b);
        }
        public override string ToString(){
            return area + " " + peri;
        }
    }
    class Program{
        static void Main(string[] args){               
            Rectangle rec = new Rectangle(2, 5);
            Console.WriteLine(rec);
        }
    }
}
