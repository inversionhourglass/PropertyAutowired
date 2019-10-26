using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void Log(Action<string> Log)
        {
            Log($"{TypeDef} sorted properties:");
            Log("\tinstance properties:");
            foreach (var prop in InstanceProps)
            {
                Log($"\t\t{prop.Order}. {prop.PropertyDef} autowired by {prop.Attribute.AttributeType} when {prop.Position}");
            }
            Log("\tstatic properties");
            foreach (var prop in StaticProps)
            {
                Log($"\t\t{prop.Order}. {prop.PropertyDef} autowired by {prop.Attribute.AttributeType} when {prop.Position}");
            }
        }
    }
}
