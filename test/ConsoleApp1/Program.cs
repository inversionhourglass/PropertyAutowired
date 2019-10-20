using Fody;
using PropertyAutowired.Fody;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var weaver = new ModuleWeaver();
            weaver.ExecuteTestRun("TestUsingAssembly.dll");

            //var wa = new WithAutowired();
            //Console.WriteLine(wa.Logger.Name);


            Console.ReadLine();
        }
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
