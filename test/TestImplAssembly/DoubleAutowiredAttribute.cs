using PropertyAutowired;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestImplAssembly
{
    public class DoubleAutowiredAttribute : AutowiredAttribute
    {
        private string _flag;

        public DoubleAutowiredAttribute(string flag, string name)
        {
            _flag = flag;
            Name = name;
        }

        public override object GetPropertyValue()
        {
            return Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
