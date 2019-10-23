using PropertyAutowired;
using System;

namespace TestImplAssembly
{
    public class GenericAutowiredAttribute : AutowiredAttribute
    {
        public override object GetPropertyValue()
        {
            return (1, "a", 3.14f, Guid.NewGuid());
        }
    }
}
