using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace statics
{
    class Student {
        public static string teacher;
        public string name;
        public Student(string name) {
            this.name = name;
        }

        static Student() {
            Console.WriteLine("set in static constructor");
            teacher = "Miss T";
        }

        public static void Hello() {
            //Console.WriteLine("my name is {0}", name);
            Console.WriteLine("my teacher is {0} \n", teacher);
        }
    }
    class Person
    {
        public static string teacher;
        static Person()
        {
            Console.WriteLine("set in static constructor");
            teacher = "Miss T";
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            StaticAttribute();
            StaticMethod();
            StaticConstructor();

            Console.ReadLine();
        }

        static void StaticAttribute() {

            Console.WriteLine(Student.teacher);

            Student s1 = new Student("nick");
            Student s2 = new Student("edwin");

            Console.WriteLine(Student.teacher);



            Console.WriteLine();
        }

        static void StaticMethod()
        {
            Student s = new Student("haha");
            Student.Hello();
        }

        static void StaticConstructor() {
            Person p = new Person();
            Person p1 = new Person();
        }

    }
}
