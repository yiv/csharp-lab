using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classvsstruct
{
    class Student {
        public string name;
        public int age;
    }
    struct Person
    {
        public string name;
        public int age;
    }
    class Program
    {
        static void Main(string[] args)
        {
            ClassIsRefType();
            ClassIsValueType();

            Console.ReadLine();
        }
        static void ClassIsRefType() {
            Student stu = new Student();
            stu.name = "hello";
            stu.age = 5;
            Console.WriteLine(stu.age);

            var other = stu;
            other.age = 50;
            Console.WriteLine(other.age);
            Console.WriteLine(stu.age);

            Console.WriteLine();
        }

        static void ClassIsValueType()
        {
            Person p = new Person();
            p.name = "hello";
            p.age = 5;
            Console.WriteLine(p.age);

            var other = p;
            other.age = 50;
            Console.WriteLine(other.age);
            Console.WriteLine(p.age);

            Console.WriteLine();
        }
    }
}
