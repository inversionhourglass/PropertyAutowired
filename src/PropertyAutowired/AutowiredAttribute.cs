using System;

namespace PropertyAutowired
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AutowiredAttribute : Attribute
    {
        /// <summary>
        /// The type which declared property
        /// </summary>
        public Type DeclaringType { get; set; }

        public abstract object GetPropertyValue();
    }
}
