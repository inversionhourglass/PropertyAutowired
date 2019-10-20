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
            Console.WriteLine(wa.Logger2.Name);
            Console.WriteLine(wa.Logger3.Name);
        }
    }
}
