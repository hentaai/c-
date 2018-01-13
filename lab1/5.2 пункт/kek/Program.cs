using System;

namespace kek
{
    class Program
    {
        static void Main(string[] args)
        {
            double r = double.Parse(Console.ReadLine());
            Circle c  = new Circle(r);
            double area = c.FindArea1();
            Console.WriteLine(c);
            Console.WriteLine(area);
        }
    }
}
