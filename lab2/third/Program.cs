using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace third
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
        public static int minprime(List<int> integers){
            bool ok =true;
            List<int> primes = new List<int> ();
            for(int i = 0; i < integers.Count; i++){
                int n = integers[i];
                ok = true;
                for(int j=2; j*j <= n; j++){
                    if(n % j == 0){
                        ok = false;
                        break;
                    }
                }
                if(ok){
                    primes.Add(integers[i]);
                }
            }
            if (primes.Count() > 0){
                int k = primes[0];
                foreach(int c in primes){
                    if(c < k){
                        k = c;
                    }    
                }
                return k;
            }
            else{
                return -1;
            }
        }
        public static void writedata(int minprime,string path){
            string s = minprime.ToString();
            File.WriteAllText(path,s);            
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
            writedata(minprime(getdata(@"/home/yerassyl/Projects/lab2/third/text.txt")),(@"/home/yerassyl/Projects/lab2/third/writtentext.txt"));

        }
    }
}
