using System;

namespace PropertyAutowired
{
    /// <summary>
    /// for common properties, don't inherit from this class
    /// </summary>
    public abstract class AbstractAutowiredAttribute : Attribute
    {
        /// <summary>
        /// The type which declared property.(value is set by framework at compile time)
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

        /// <summary>
        /// Properties initialize order
        /// </summary>
        public int Order { get; set; }


        /// <summary>
        /// property initialize position or timing
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// return a value for the property
        /// </summary>
        public abstract object GetPropertyValue();
    }
}
