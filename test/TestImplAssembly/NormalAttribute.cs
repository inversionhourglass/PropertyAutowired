using System;
using System.Reflection;

namespace TestImplAssembly
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
    public class NormalAttribute : Attribute
    {
        public BindingFlags[] PropertyFlags { get; set; }
        public Type[] ExceptedDeclaringTypes { get; set; }
        public Type[] TargetPropertyTypes { get; set; }
        public Type DeclaringType { get; set; }

        public object GetPropertyValue()
        {
            return null;
        }
    }
}
