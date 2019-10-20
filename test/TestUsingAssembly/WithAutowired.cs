using TestImplAssembly;
using TestImplAssembly.Logging;

namespace TestUsingAssembly
{
    public class WithAutowired
    {
        static WithAutowired() { }

        public string Name { get; set; }

        public string[] Names { get; set; }

        public int[] Arr { get; set; }

        [CtorAutowired("object", true, 'v', 1, 2, 3, 4, 5f, 6d, 7, 8, 9, "ctor", typeof(ILogable), TestEnum.B, "x", "y",
            "z")]
        public ILogger Logger1 { get; }
        //= (ILogger)new CtorAutowiredAttribute("object", true, 'v', 1, 2, 3, 4, 5f, 6d,
        //7, 8, 9, "ctor", typeof(ILogable), TestEnum.B, "x", "y", "z"){TargetType = typeof(WithAutowired)}.GetPropertyValue();

        [PropAutowired(Object = typeof(TestAutowiredAttribute), Bool = true, Char = 'o', Byte = 11, Short = 22,
            Int = 33, Long = 44, Float = 55f, Double = 66d, Ushort = 77, Uint = 88, Ulong = 99, String = "prop",
            Type = typeof(PropertyAutowired.AutowiredAttribute), Enum = TestEnum.C, Array = new[] { "k", "m", "n" })]
        public static ILogger Logger2 { get; }
        //= (ILogger)new PropAutowiredAttribute
        //{
        //    TargetType = typeof(WithAutowired),
        //    Object = typeof(TestAutowiredAttribute),
        //    Bool = true,
        //    Char = 'o',
        //    Byte = 11,
        //    Short = 22,
        //    Int = 33,
        //    Long = 44,
        //    Float = 55f,
        //    Double = 66d,
        //    Ushort = 77,
        //    Uint = 88,
        //    Ulong = 99,
        //    String = "prop",
        //    Type = typeof(PropertyAutowired.AutowiredAttribute),
        //    Enum = TestEnum.C,
        //    Array = new[] { "k", "m", "n" }
        //}.GetPropertyValue();

        [CaPAutowired(true, 111, 333, 555f, 777, 999, typeof(WithAutowired), "a", "b", "c", Char = 'u',
            Object = new[] { 111 }, Short = 222, Long = 444, Double = 666d, Uint = 888, String = "ctor and prop",
            Enum = TestEnum.A)]
        public ILogger Logger3 { get; }
        //= (ILogger)new CaPAutowiredAttribute(true, 111, 333, 555f, 777, 999, typeof(WithAutowired), "a", "b", "c")
        //{
        //    TargetType = typeof(WithAutowired),
        //    Char = 'u',
        //    Object = new[] { 111 },
        //    Short = 222,
        //    Long = 444,
        //    Double = 666d,
        //    Uint = 888,
        //    String = "ctor and prop",
        //    Enum = TestEnum.A
        //}.GetPropertyValue();
    }
}
