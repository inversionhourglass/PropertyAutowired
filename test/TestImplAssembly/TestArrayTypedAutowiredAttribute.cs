using PropertyAutowired;

namespace TestImplAssembly
{
    public class TestArrayTypedAutowiredAttribute : TypedAutowiredAttribute
    {
        public override object GetPropertyValue()
        {
            return new[] { 971 };
        }
    }
}
