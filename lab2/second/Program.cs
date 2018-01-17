using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace second
{
    class Program
    {       
        public static List<int> getdata(string path){
            //FileInfo a = new FileInfo(path);
            string text = File.ReadAllText(path);
            List<int> a = new List<int> ();
            string[] s = text.Split(' ');
            for(int i = 0;i < s.Length ; i++){
                a.Add(int.Parse(s[i]));
            }
            return a;
        }
        public static int getMax(List<int> integers){
            int k = 0;
            foreach(int c in integers){
                if(c > k){
                    k = c;
                }
            }
            return k;
        }

        public static int getMin(List<int> integers){   
            int k = integers[0];
            foreach(int c in integers){
                if(c < k){
                    k = c;
                }
            }
            return k;
        }


        static void Main(string[] args)
        {
            int max = getMax(getdata(@"/home/yerassyl/Projects/lab2/second/text.txt"));
            int min = getMin(getdata(@"/home/yerassyl/Projects/lab2/second/text.txt"));
            Console.WriteLine(max);
            Console.WriteLine(min);
        }
    }
}
