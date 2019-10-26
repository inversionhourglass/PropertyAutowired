using Mono.Cecil;
using System.Linq;

namespace PropertyAutowired.Fody
{
    internal sealed class AutowiredProperties
    {
        public AutowiredProperties(PropertyDefinition propertyDef, CustomAttribute attribute, AutowiredWays ways)
        {
            PropertyDef = propertyDef;
            Attribute = attribute;
            Ways = ways;
            foreach (var prop in attribute.Properties)
            {
                switch (prop.Name)
                {
                    case Consts.Autowired_Position:
                        Position = (Position)prop.Argument.Value;
                        break;
                    case Consts.Autowired_Order:
                        Order = (int)prop.Argument.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        public PropertyDefinition PropertyDef { get; set; }

        public CustomAttribute Attribute { get; set; }

        public int Order { get; set; } = 0;

        public Position Position { get; set; } = Position.FirstOfAll;

        public AutowiredWays Ways { get; set; }
    }
}
