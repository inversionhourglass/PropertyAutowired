using PropertyAutowired;
using TestImplAssembly.Logging;

namespace TestImplAssembly
{
    public class TypedLoggerAutowiredAttribute : TypedAutowiredAttribute
    {
        public override object GetPropertyValue()
        {
            return new DefaultLogger("default");
        }
    }
}
