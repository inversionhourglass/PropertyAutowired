using PropertyAutowired;
using System;

namespace TestImplAssembly
{
    public class TestAutowiredAttribute : AutowiredAttribute
    {
        public TestAutowiredAttribute(Type obj)
        //public TestAutowiredAttribute(object obj)
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public override object GetPropertyValue()
        {
            return $"{TargetType.Name}.{Name}";
        }
    }
}
