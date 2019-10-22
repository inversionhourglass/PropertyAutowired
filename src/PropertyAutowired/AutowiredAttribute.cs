using System;

namespace PropertyAutowired
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AutowiredAttribute : Attribute
    {
        /// <summary>
        /// The type which declared property.(value set by framework at compile time)
        /// </summary>
        [Obsolete("do not set this property, it will set by framework at compile time, you can use it in GetPropertyValue method")]
        public Type DeclaringType { get; set; }

        public abstract object GetPropertyValue();
    }
}
