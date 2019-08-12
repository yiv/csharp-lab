using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace functions
{
    class Student
    {
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
            OptionParams("a little");
            OptionParams("a little", "not default");
            OptionParams(a:"a little", c:3);


            Student stu = new Student();
            stu.name = "nick";
            stu.age = 5;
            PassRefTypeByRef(ref stu);
            Console.WriteLine("{0}, {1}",stu.name, stu.age);

            Console.ReadLine();
        }

        static void OptionParams(string a , string b = "default string", int c = 100)
        {
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.WriteLine();
        }

        static void PassRefTypeByValue(Student stu) {
            stu.age = 50;
            var other = new Student();
            other.name = "edwin";
            other.age = 500;
            stu = other;
        }

        static void PassRefTypeByRef(ref Student stu)
        {
            stu.age = 50;
            var other = new Student();
            other.name = "edwin";
            other.age = 500;
            stu = other;
        }

    }
}
