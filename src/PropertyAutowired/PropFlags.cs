using System;

#if ENUM_COPIES
namespace PropertyAutowired.Fody
#else
namespace PropertyAutowired
#endif
{
    [Flags]
#if ENUM_COPIES
    internal enum PropFlags
#else
    public enum PropFlags
#endif
    {
        Default = 0,
        Instance = 4,
        Static = 8,
        Public = 16,
        NonPublic = 32
    }
}
