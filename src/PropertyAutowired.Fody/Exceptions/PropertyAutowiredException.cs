using Fody;
using System;

namespace PropertyAutowired.Fody
{
    public class PropertyAutowiredException : WeavingException
    {
        public PropertyAutowiredException(string message) : base(message) { }
    }
}
