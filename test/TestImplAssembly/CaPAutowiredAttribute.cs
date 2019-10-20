using System;

namespace TestImplAssembly
{
    public class CaPAutowiredAttribute : TestAutowiredAttribute
    {
        public CaPAutowiredAttribute(bool boolean, byte bt, int i4, float f, ushort ush, ulong ui8, Type type, params string[] arr)
        {
            Bool = boolean;
            Byte = bt;
            Int = i4;
            Float = f;
            Ushort = ush;
            Ulong = ui8;
            Type = type;
            Array = arr;
        }
    }
}
