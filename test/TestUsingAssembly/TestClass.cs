using System;
using TestImplAssembly;

namespace TestUsingAssembly
{
    public class TestClass
    {
        [TestAutowired]
        public string Name { get; set; }
    }
}
