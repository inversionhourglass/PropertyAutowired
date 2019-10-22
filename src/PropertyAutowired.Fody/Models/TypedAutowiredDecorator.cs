using Mono.Cecil;
using System.Linq;
using System.Reflection;

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
                            PropertyFlags = ((CustomAttributeArgument[])prop.Argument.Value).Select(v => (BindingFlags)v.Value).ToArray();
                            if (PropertyFlags.Length > 0) break;
                        }
                        PropertyFlags = new[] { BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static };
                        break;
                    case Consts.TypedAutowiredAttribute_ExceptedDeclaringTypes:
                        if (prop.Argument.Value == null) break;
                        ExceptedDeclaringTypes = ((CustomAttributeArgument[])prop.Argument.Value).Select(caa => ((TypeReference)caa.Value).Resolve()).ToArray();
                        break;
                    case Consts.TypedAutowiredAttribute_TargetPropertyTypes:
                        if (prop.Argument.Value == null) break;
                        TargetPropertyTypes = ((CustomAttributeArgument[])prop.Argument.Value).Select(caa => ((TypeReference)caa.Value).Resolve()).ToArray();
                        break;
                    default:
                        break;
                }
            }
        }

        public CustomAttribute Attribute { get; set; }

        public BindingFlags[] PropertyFlags { get; }

        public TypeDefinition[] ExceptedDeclaringTypes { get; }

        public TypeDefinition[] TargetPropertyTypes { get; }
    }
}
