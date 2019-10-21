using PropertyAutowired;
using System;
using TestImplAssembly.Logging;

namespace TestImplAssembly
{
    public abstract class TestAutowiredAttribute : AutowiredAttribute
    {
        public object Object { get; set; }

        public bool Bool { get; set; }

        public char Char { get; set; }

        public byte Byte { get; set; }

        public short Short { get; set; }

        public int Int { get; set; }

        public long Long { get; set; }

        public float Float { get; set; }

        public double Double { get; set; }

        public ushort Ushort { get; set; }

        public uint Uint { get; set; }

        public ulong Ulong { get; set; }

        public string String { get; set; }

        public Type Type { get; set; }

        public TestEnum Enum { get; set; }

        public string[] Array { get; set; }

        public override object GetPropertyValue()
        {
            Console.WriteLine("{");
            Console.WriteLine($"\t{nameof(Object)}: {Object}");
            Console.WriteLine($"\t{nameof(Bool)}: {Bool}");
            Console.WriteLine($"\t{nameof(Char)}: {Char}");
            Console.WriteLine($"\t{nameof(Byte)}: {Byte}");
            Console.WriteLine($"\t{nameof(Short)}: {Short}");
            Console.WriteLine($"\t{nameof(Int)}: {Int}");
            Console.WriteLine($"\t{nameof(Long)}: {Long}");
            Console.WriteLine($"\t{nameof(Float)}: {Float}");
            Console.WriteLine($"\t{nameof(Double)}: {Double}");
            Console.WriteLine($"\t{nameof(Ushort)}: {Ushort}");
            Console.WriteLine($"\t{nameof(Uint)}: {Uint}");
            Console.WriteLine($"\t{nameof(Ulong)}: {Ulong}");
            Console.WriteLine($"\t{nameof(String)}: {String}");
            Console.WriteLine($"\t{nameof(Type)}: {Type}");
            Console.WriteLine($"\t{nameof(Enum)}: {Enum}");
            Console.WriteLine($"\t{nameof(Array)}: {string.Join(",", Array)}");
            Console.WriteLine("}");
            return LogFactory.GetLogger(DeclaringType.FullName);
        }
    }
}
