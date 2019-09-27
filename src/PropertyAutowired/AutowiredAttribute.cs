using System;

namespace PropertyAutowired
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AutowiredAttribute : Attribute
    {
        public abstract object GetPropertyValue();
    }
}
