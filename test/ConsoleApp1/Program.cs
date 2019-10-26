using Fody;
using PropertyAutowired.Fody;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var value = (ValueTuple<int, string, float, Guid>)GetTuple();
            //var int4 = (int)GetInt();
            //var guid = (Guid)GetGuid();
            //Console.WriteLine("item1: " + value.Item1);
            //Console.WriteLine("item2: " + value.Item2);
            //Console.WriteLine("item3: " + value.Item3);
            //Console.WriteLine("item4: " + value.Item4);
            var weaver = new ModuleWeaver();
            weaver.ExecuteTestRun("TestUsingAssembly.dll");

            //var wa = new WithAutowired();
            //Console.WriteLine(wa.Logger.Name);

            Console.WriteLine("Press entry to continue...");
            Console.ReadLine();
        }

        static object GetTuple()
        {
            return (1, "a", 3.14f, Guid.NewGuid());
        }

        static object GetInt() => 1;

        static object GetGuid() => Guid.NewGuid();
    }

    abstract class A
    {
        public abstract Type X { get; set; }
    }

    class B : A
    {
        public override Type X { get; set; }

        public virtual Type Y { get; set; }
    }
}
