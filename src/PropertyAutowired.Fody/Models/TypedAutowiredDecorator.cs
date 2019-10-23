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
            if (PropertyFlags == null) PropertyFlags = new[] { BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static };
        }

        public CustomAttribute Attribute { get; set; }

        public BindingFlags[] PropertyFlags { get; }

        public string[] ExceptedDeclaringTypes { get; }

        public string[] TargetPropertyTypes { get; }
    }
}
