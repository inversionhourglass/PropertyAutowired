﻿using Mono.Cecil;
using System.Collections.Generic;

namespace PropertyAutowired.Fody
{
    internal sealed class AutowiredType : IComparer<AutowiredProperties>
    {
        public AutowiredType(TypeDefinition typeDef)
        {
            TypeDef = typeDef;
            InstanceProps = new List<AutowiredProperties>();
            StaticProps = new List<AutowiredProperties>();
        }

        public TypeDefinition TypeDef { get; set; }

        public List<AutowiredProperties> InstanceProps { get; set; }

        public List<AutowiredProperties> StaticProps { get; set; }

        public bool HasAutowired => InstanceProps.Count > 0 || StaticProps.Count > 0;

        public int Compare(AutowiredProperties x, AutowiredProperties y)
        {
            return x.Order.CompareTo(y.Order);
        }

        public void Sort()
        {
            InstanceProps.Sort(this);
            StaticProps.Sort(this);
        }
    }
}
