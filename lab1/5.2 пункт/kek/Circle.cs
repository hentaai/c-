using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kek
{
    public class Circle
    {
        public double radius;
        public double area;
        public Circle(){
            radius = 10;
        }
        public Circle(double a){
            radius = a;
        }
       /* public void FindArea(){
            area = Math.PI * radius * radius;
        }*/
        public double FindArea1(){
            return Math.PI * radius * radius;
        }
        public override string ToString(){
            return radius.ToString();
        }
    }
}
