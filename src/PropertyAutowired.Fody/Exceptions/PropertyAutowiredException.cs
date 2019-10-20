using System;

namespace PropertyAutowired.Fody
{
    public class PropertyAutowiredException : Exception
    {
        public PropertyAutowiredException(string message) : base(message) { }
    }
}
