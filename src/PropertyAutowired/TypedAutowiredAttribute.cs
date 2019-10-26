using System;

namespace PropertyAutowired
{
    /// <summary>
    /// Declare the property types that need to be autowired by <see cref="TargetPropertyTypes"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module)]
    public abstract class TypedAutowiredAttribute : AbstractAutowiredAttribute
    {
        /// <summary>
        /// (one of the ProFlags array) &amp; property.BindingFlags == (one of the ProFlags array)
        /// </summary>
        public PropFlags[] PropertyFlags { get; set; }

        /// <summary>
        /// excepted declaring types.(optional)
        /// </summary>
        public Type[] ExceptedDeclaringTypes { get; set; }

        /// <summary>
        /// Property types. the property type have to equals one of type array, not support subclass.
        /// </summary>
        public Type[] TargetPropertyTypes { get; set; }
    }
}
