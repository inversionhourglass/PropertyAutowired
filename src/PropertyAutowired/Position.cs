#if ENUM_COPIES
namespace PropertyAutowired.Fody
#else
namespace PropertyAutowired
#endif
{
#if ENUM_COPIES
    internal enum Position
#else
    public enum Position
#endif
    {
        FirstOfAll,
        /// <summary>
        /// equals with <see cref="EndOfConstructor"/> if apply to static constructor
        /// </summary>
        AfterDefaultInit,
        /// <summary>
        /// equals with <see cref="EndOfConstructor"/> if apply to static constructor
        /// </summary>
        AfterBaseConstructor,
        EndOfConstructor
    }
}
