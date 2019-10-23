using PropertyAutowired;
using System;
using TestImplAssembly;
using TestImplAssembly.Logging;

namespace TestUsingAssembly
{
    public class TypedTModel1
    {
        public TypedTModel1()
        {
            Console.WriteLine($@"
{nameof(TypedTModel1)}{{
    A: {A},
    B: {B},
    C: {C},
    D: {D},
    E: {E},
    F: {F},
    G: {G},
    Logger: {Logger.Name},
    DLogger: {DLogger.Name}
}}");
        }

        public static int A { get; set; }

        public long B { get; set; }

        internal static float C { get; set; }

        protected static double D { get; set; }

        private static double E { get; set; }

        public static double F { get; set; }

        [IgnoreAutowired]
        private static double G { get; set; }

        public ILogger Logger { get; set; }

        public static DefaultLogger DLogger { get; set; }
    }

    public class TypedTModel2
    {
        public TypedTModel2()
        {
            Console.WriteLine($@"
{nameof(TypedTModel1)}{{
    A: {A},
    B: {B},
    C: {C},
    D: {D},
    Func: {Func(typeof(MemoryExtensions))},
    Value: item1: {Value.Item1}, item2: {Value.Item2}, item3: {Value.Item3}, item4: {Value.Item4}
}}");
        }
        public static int A { get; set; }

        [IgnoreAutowired]
        public long B { get; set; }

        internal static float C { get; set; }

        protected static double D { get; set; }

        public Func<Type, string> Func { get; set; }

        [GenericAutowired]
        public (int, string, float, Guid) Value { get; set; }
    }

    [IgnoreAutowired]
    public class TypedTModel3
    {
        public TypedTModel3()
        {
            Console.WriteLine($@"
{nameof(TypedTModel1)}{{
    A: {A},
    B: {B},
    C: {C},
    D: {D}
}}");
        }
        public int A { get; set; }

        public long B { get; set; }

        public float C { get; set; }

        public double D { get; set; }
    }
}
