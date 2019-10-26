using System;

namespace PropertyAutowired
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AutowiredAttribute : Attribute
    {
        /// <summary>
        /// The type which declared property.(value set by framework at compile time)
        /// </summary>
#if !NETSTANDARD2_1
            [Obsolete("do not set this property, it will set by framework at compile time, you can use it in GetPropertyValue method")]
#endif
        public Type DeclaringType
        {
            get;
#if NETSTANDARD2_1
            [Obsolete("do not set this property, it will set by framework at compile time, you can use it in GetPropertyValue method")]
#endif
            set;
        }

        public int Order { get; set; }

        public Position Position { get; set; }

        public abstract object GetPropertyValue();
    }
}
