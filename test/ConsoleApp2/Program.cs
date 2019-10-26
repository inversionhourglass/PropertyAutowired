using System;
using TestUsingAssembly;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var wa = new WithAutowired();
            Console.WriteLine(wa.Logger1.Name);
            Console.WriteLine(WithAutowired.Logger2.Name);
            Console.WriteLine(wa.Logger3.Name);
            new TypedTModel1();
            new TypedTModel2();
            new TypedTModel3();
        }
    }
}
