using PropertyAutowired;
using System;

namespace TestImplAssembly
{
    public class TypedGenericAutowiredAttribute : TypedAutowiredAttribute
    {
        public override object GetPropertyValue()
        {
            return (Func<Type, string>)GetFullNameWithToken;
        }

        public string GetFullNameWithToken(Type type) => $"{type.FullName}-{type.MetadataToken}";
    }
}
