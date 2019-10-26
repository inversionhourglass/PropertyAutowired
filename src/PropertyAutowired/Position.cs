#if ENUM_COPIES
namespace PropertyAutowired.Fody
#else
namespace PropertyAutowired
#endif
{
    /// <summary>
    /// where to weave the code
    /// </summary>
#if ENUM_COPIES
    internal enum Position
#else
    public enum Position
#endif
    {
        /// <summary>
        /// first of all
        /// </summary>
        FirstOfAll,
        /// <summary>
        /// equals with <see cref="EndOfConstructor"/> if apply to static constructor
        /// </summary>
        AfterDefaultInit,
        /// <summary>
        /// equals with <see cref="EndOfConstructor"/> if apply to static constructor
        /// </summary>
        AfterBaseConstructor,
        /// <summary>
        /// before constructor return
        /// </summary>
        EndOfConstructor
    }
}
