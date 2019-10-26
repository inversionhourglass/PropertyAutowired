using System;

namespace PropertyAutowired
{
    /// <summary>
    /// Single property autowired
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class AutowiredAttribute : AbstractAutowiredAttribute
    {
    }
}
