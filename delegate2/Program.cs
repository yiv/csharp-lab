using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace delegate2
{
    class Owner {
        string name;
        public void DoSomeThingForMe(string yourName) {
            Console.WriteLine("I am {0}, thank {1} do something for me", name, yourName);
        }
        public Owner(string name = "owner1") {
            this.name = name;
        }
    }
    class Delegater {
        string name;
        public DelegateFun myDelegateFun;

        public delegate void DelegateFun(string name);
        public event DelegateFun EventDoThing;

        public Delegater()
        {
            name = "delegater1";
        }
        public Delegater(DelegateFun fun) : this() {
            myDelegateFun = fun;
        }
        public void DoingWork() {
            Thread.Sleep(1000);
            this.myDelegateFun(this.name);
            if (this.EventDoThing != null) {
                this.EventDoThing(this.name);
            }
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DoWhenIamLive();

            DoAfterIamDead();

            Console.ReadLine();
        }
        static void DoWhenIamLive() {
            Owner owner = new Owner();
            Delegater delegater = new Delegater(owner.DoSomeThingForMe);
            delegater.EventDoThing += owner.DoSomeThingForMe;
            delegater.EventDoThing += delegate (string name)
            {
                Console.WriteLine("anonymous");
            };
            delegater.DoingWork();
        }

        static void DoAfterIamDead()
        {
            
            Delegater delegater = new Delegater();
            {
                Owner owner = new Owner();
                delegater.myDelegateFun += owner.DoSomeThingForMe;
            }
            delegater.DoingWork();
        }
    }
}
