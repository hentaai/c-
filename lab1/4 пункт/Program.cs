using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Student{
        public string name;
        public string faculty;
        public int course;
        public int age;
        public double gpa;
        public Student(string a, string b, int c, int d, double e){
            name = a;
            faculty = b;
            course = c;
            age = d;
            gpa = e;
        }
        public override string ToString(){
            return name + "\n" + faculty + "\n" + course + "\n" + age + "\n" + gpa;
        }
    }
    class Program{
        static void Main(string[] args){
            Student Yera = new Student("Yera", "FIT", 1, 17, 2.86);
            Console.WriteLine(Yera);
        }
    }
}