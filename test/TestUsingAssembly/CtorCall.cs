using System;
using System.Collections.Generic;
using System.Text;

namespace TestUsingAssembly
{
    class CtorCall
    {
        public CtorCall(int a) : this(a.ToString())
        {

        }

        public CtorCall(params string[] ss)
        {
            foreach (var s in ss)
            {
                Console.WriteLine(s);
            }
        }
    }
}
