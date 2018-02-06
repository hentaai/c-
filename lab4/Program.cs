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
        static XmlSerializer Serializerr(){
            XmlSerializer xs = new XmlSerializer(typeof(Complex));
            FileStream fs = new FileStream("data.xml", FileMode.OpenOrCreate);
            Complex c = new Complex (0,0);
            xs.Serialize(fs ,c);
            fs.Close();
            return xs;
        }
        static void Deserializerr(XmlSerializer xs){
            XmlSerializer dxs = new XmlSerializer(typeof(Complex));
            FileStream dfs = new FileStream("data.xml", FileMode.Open);
            Complex d = dxs.Deserialize(dfs) as Complex;
            Console.WriteLine(d.a.ToString());
            Console.WriteLine(d.b.ToString());
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
            Deserializerr(Serializerr());
        }
    }
}
