using System;

#if ENUM_COPIES
namespace PropertyAutowired.Fody
#else
namespace PropertyAutowired
#endif
{
    /// <summary>
    /// Property BindingFlags
    /// </summary>
    [Flags]
#if ENUM_COPIES
    internal enum PropFlags
#else
    public enum PropFlags
#endif
    {
        /// <summary>
        /// Default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Instance
        /// </summary>
        Instance = 4,
        /// <summary>
        /// Static
        /// </summary>
        Static = 8,
        /// <summary>
        /// Public
        /// </summary>
        Public = 16,
        /// <summary>
        /// NonPublic
        /// </summary>
        NonPublic = 32,
        /// <summary>
        /// InstancePublic
        /// </summary>
        InstancePublic = Instance | Public,
        /// <summary>
        /// InstanceNonPublic
        /// </summary>
        InstanceNonPublic = Instance | NonPublic,
        /// <summary>
        /// StaticPublic
        /// </summary>
        StaticPublic = Static | Public,
        /// <summary>
        /// StaticNonPublic
        /// </summary>
        StaticNonPublic = Static | NonPublic
    }
}
