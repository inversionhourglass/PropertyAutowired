using PropertyAutowired;
using System;

namespace TestImplAssembly
{
    public class TestAutowiredAttribute : AutowiredAttribute
    {
        public override object GetPropertyValue()
        {
            return "enheng";
        }
    }
}
