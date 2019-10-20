using PropertyAutowired;
using System;
using TestImplAssembly.Logging;

namespace TestImplAssembly
{
    public class CtorAutowiredAttribute : TestAutowiredAttribute
    {
        public CtorAutowiredAttribute(object obj, bool boolean, char ch, byte bt, short sh, int i4, long i8, float f, double d, ushort ush, uint ui4, ulong ui8, string str, Type type, TestEnum @enum, params string[] arr)
        {
            Object = obj;
            Bool = boolean;
            Char = ch;
            Byte = bt;
            Short = sh;
            Int = i4;
            Long = i8;
            Float = f;
            Double = d;
            Ushort = ush;
            Uint = ui4;
            Ulong = ui8;
            String = str;
            Type = type;
            Enum = @enum;
            Array = arr;
        }
    }
}
