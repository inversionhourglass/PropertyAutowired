using PropertyAutowired;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestUsingAssembly
{
    class TypedTModel1
    {
        public static int A { get; set; }

        public long B { get; set; }

        internal static float C { get; set; }

        protected static double D { get; set; }

        private static double E { get; set; }

        public static double F { get; set; }

        [IgnoreAutowired]
        private static double G { get; set; }
    }

    class TypedTModel2
    {
        public static int A { get; set; }

        [IgnoreAutowired]
        public long B { get; set; }

        internal static float C { get; set; }

        protected static double D { get; set; }
    }

    [IgnoreAutowired]
    class TypedTModel3
    {
        public int A { get; set; }

        public long B { get; set; }

        public float C { get; set; }

        public double D { get; set; }
    }
}
