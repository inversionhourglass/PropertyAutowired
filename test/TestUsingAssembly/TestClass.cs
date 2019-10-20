using System;
using TestImplAssembly;

namespace TestUsingAssembly
{
    public abstract class TestClass
    {
        public string Name { get; set; }

        //[DoubleAutowired(Name = nameof(TestClass))]
        public string Aleilei { get; set; }
        
        //[TestAutowired(B.A)]
        public abstract string Enhengheng { get; set; }

        //[DoubleAutowired(Name = "enheng")]
        public static string Oho { get; set; }
    }

    public class A
    {
        public bool Bool { get; set; } = true;

        public char Char { get; set; } = '0';

        public byte Byte { get; set; } = 100;

        public short Short { get; set; } = 200;

        public int Int { get; set; } = 300;

        public long Long { get; set; } = 4L;

        public float Float { get; set; } = 500;

        public double Double { get; set; } = 600;

        public ushort Ushort { get; set; } = 700;

        public uint Uint { get; set; } = 800;

        public ulong Ulong { get; set; } = long.MaxValue;

        public string String { get; set; } = "11";

        public Type Type { get; set; } = typeof(C);

        public Type Type2 { get; set; } = C.Type;

        public B Enum { get; set; } = B.K;

        public string[] Array { get; set; } = new [] { "z", "v", "x" };

        public C Cs { get; set; }
    }

    public enum B
    {
        A,
        K
    }

    public class C
    {
        static C()
        {
            Console.WriteLine(123);
        }

        public static Type Type = typeof(C);
    }
}
