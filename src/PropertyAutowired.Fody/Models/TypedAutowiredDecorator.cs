using Mono.Cecil;
using System.Linq;

namespace PropertyAutowired.Fody
{
    internal class TypedAutowiredDecorator
    {
        public TypedAutowiredDecorator(CustomAttribute attribute)
        {
            Attribute = attribute;

            foreach (var prop in attribute.Properties)
            {
                switch (prop.Name)
                {
                    case Consts.TypedAutowiredAttribute_PropertyFlags:
                        if(prop.Argument.Value != null)
                        {
                            PropertyFlags = ((CustomAttributeArgument[])prop.Argument.Value).Select(v => (PropFlags)v.Value).ToArray();
                        }
                        break;
                    case Consts.TypedAutowiredAttribute_ExceptedDeclaringTypes:
                        if (prop.Argument.Value == null) break;
                        ExceptedDeclaringTypes = ((CustomAttributeArgument[])prop.Argument.Value).Select(caa => ((TypeReference)caa.Value).FullName).ToArray();
                        break;
                    case Consts.TypedAutowiredAttribute_TargetPropertyTypes:
                        if (prop.Argument.Value == null) break;
                        TargetPropertyTypes = ((CustomAttributeArgument[])prop.Argument.Value).Select(caa => ((TypeReference)caa.Value).FullName).ToArray();
                        break;
                    default:
                        break;
                }
            }
        }

        public CustomAttribute Attribute { get; set; }

        public PropFlags[] PropertyFlags { get; } = new[] { PropFlags.Public | PropFlags.Instance | PropFlags.Static };

        public string[] ExceptedDeclaringTypes { get; }

        public string[] TargetPropertyTypes { get; }
    }
}
