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


            Console.ReadLine();
        }
    }

    public interface IA
    {
        string Name { get => this.GetType().Name; }
    }

    public interface IB
    {
        int Code { get => this.GetType().GetHashCode(); }
    }

    public class The : IA, IB
    {
        string IA.Name { get => "wa"; }
    }
}
