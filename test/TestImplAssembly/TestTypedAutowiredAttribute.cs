using PropertyAutowired;

namespace TestImplAssembly
{
    public class TestTypedAutowiredAttribute : TypedAutowiredAttribute
    {
        public override object GetPropertyValue()
        {
            return 971;
        }
    }
}
