using System;
using System.Collections.Generic;
using System.Text;

namespace TestUsingAssembly
{
    public class NonZeroConstructor
    {
        public NonZeroConstructor(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public Guid Identity { get; set; } = Guid.NewGuid();
    }
}
