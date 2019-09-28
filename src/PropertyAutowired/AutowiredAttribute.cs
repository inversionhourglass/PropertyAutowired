using System;

namespace PropertyAutowired
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AutowiredAttribute : Attribute
    {
        public Type TargetType { get; set; }

        public abstract object GetPropertyValue();
    }
}
