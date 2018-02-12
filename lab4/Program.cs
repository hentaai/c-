using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;


namespace lab4
{
    class Program
    {
        static XmlSerializer Serializerr(Complex o){
            XmlSerializer xs = new XmlSerializer(typeof(Complex));
            FileStream fs = new FileStream("data.xml", FileMode.OpenOrCreate);
            xs.Serialize(fs ,o);
            fs.Close();
            return xs;
        }
        static void Deserializerr(XmlSerializer xs){
            XmlSerializer xfs = new XmlSerializer(typeof(Complex));
            FileStream dfs =  new FileStream("data.xml", FileMode.Open,FileAccess.ReadWrite);
            Complex l = xfs.Deserialize(dfs) as Complex;
            Console.WriteLine(l.a);
            Console.WriteLine(l.b);
            dfs.Close();
        }
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            string c = s.Split(' ')[0];
            string d = s.Split(' ')[1];
            int a_ = int.Parse(c.Split('/')[0]);
            int b_ = int.Parse(c.Split('/')[1]);
            int c_ = int.Parse(d.Split('/')[0]);
            int d_ = int.Parse(d.Split('/')[1]);
            Complex a = new Complex(a_, b_);
            Complex b = new Complex(c_, d_);
            Complex kek = a + b;
            Deserializerr(Serializerr(kek));
        }
    }
}
