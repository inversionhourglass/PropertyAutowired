using Mono.Cecil;

namespace PropertyAutowired.Fody
{
    internal sealed class AutowiredProperties
    {
        public AutowiredProperties(PropertyDefinition propertyDef, CustomAttribute attribute)
        {
            PropertyDef = propertyDef;
            Attribute = attribute;
        }

        public PropertyDefinition PropertyDef { get; set; }

        public CustomAttribute Attribute { get; set; }
    }
}
