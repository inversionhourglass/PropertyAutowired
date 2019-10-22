using Mono.Cecil;

namespace PropertyAutowired.Fody
{
    internal sealed class AutowiredProperties
    {
        public AutowiredProperties(PropertyDefinition propertyDef, CustomAttribute attribute, AutowiredWays ways)
        {
            PropertyDef = propertyDef;
            Attribute = attribute;
            Ways = ways;
        }

        public PropertyDefinition PropertyDef { get; set; }

        public CustomAttribute Attribute { get; set; }

        public AutowiredWays Ways { get; set; }
    }
}
