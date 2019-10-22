using System;

namespace PropertyAutowired
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class IgnoreAutowiredAttribute : Attribute
    {
    }
}
