using System;
using System.Reflection;

namespace PropertyAutowired
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
    public abstract class TypedAutowiredAttribute : Attribute
    {
        /// <summary>
        /// TargetProperty.BindingFlags & PropertyFlags == TargetProperty.BindingFlags.(now support public, nonpublic, static, instance)
        /// </summary>
        public BindingFlags[] PropertyFlags { get; set; }

        /// <summary>
        /// excepted declaring types.
        /// </summary>
        public Type[] ExceptedDeclaringTypes { get; set; }

        /// <summary>
        /// Property types.(have to set this property when use it, do not make a default value at implement type)
        /// </summary>
        public Type[] TargetPropertyTypes { get; set; }

        /// <summary>
        /// The type which declared property.(value set by framework at compile time)
        /// </summary>
        [Obsolete("do not set this property, it will set by framework at compile time, you can use it in GetPropertyValue method")]
        public Type DeclaringType { get; set; }

        public abstract object GetPropertyValue();
    }
}
