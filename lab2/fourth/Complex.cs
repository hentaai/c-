using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace fourth
{
    class Complex
    {
        public int a,b;
        public Complex(int _a, int _b){
            a = _a;
            b = _b;
        }
        public override string ToString(){
            return  a + "/" + b;
        }
        public static Complex operator +(Complex c1, Complex c2){
            int max = Math.Max(c1.b,c2.b);
            int min = Math.Min(c1.b,c2.b);

            int hcf = gcd(min,max);
            int lcm  = (min * max) / hcf;
            int k = lcm / c1.b;
            int l = lcm / c2.b;
            int a_ = c1.a * k + c2.a * l;
            int b_ = k * c1.b;
            Complex c = new Complex(a_,b_);
            c.Simplify();
            return c;
        }
        public static int gcd(int x, int y){
            if(y == 0){
                return x;
            }
            return gcd(y, x % y);
        }
        public void Simplify(){
            int _a = this.a;
            int _b = this.b;
            while(_a > 0 && _b > 0){
                if(_a > _b){
                    _a = _a % _b;
                }
                else{
                    _b = _b % _a;
                }
            }
            int nod = _a + _b;
            a /= nod;
            b /= nod;  
        }
    }
}