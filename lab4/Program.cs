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
        static XmlSerializer Serializerr(int a,int b){
            XmlSerializer xs = new XmlSerializer(typeof(Complex));
            FileStream fs = new FileStream("data.xml", FileMode.OpenOrCreate);
            Complex c = new Complex (a,b);
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
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            Deserializerr(Serializerr(a,b));
        }
    }
}
