using System;

namespace PropertyAutowired
{
    /// <summary>
    /// Invalid for AutowiredAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class IgnoreAutowiredAttribute : Attribute
    {
    }
}
