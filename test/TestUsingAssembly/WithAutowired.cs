using System;
using System.Collections.Generic;
using System.Text;
using TestImplAssembly;

namespace TestUsingAssembly
{
    class WithAutowired
    {
        public WithAutowired()
        {
            Name = (string)new DoubleAutowiredAttribute("NewName", "abb") { Id = 12 }.GetPropertyValue();
        }

        public string Name { get; set; }

        //[TestAutowired("NewName", new[] { "abb", "12", "bba" }, Id = 12)]
        public string[] Names { get; set; } = new[] { "aba", "21", "bab" };

        public int[] Arr { get; set; } = new[] { 1, 2, 3 };
}
}
