using System.Collections.Generic;
using Mono.Cecil;

namespace PropertyAutowired.Fody
{
    public partial class ModuleWeaver
    {
        private void FindAutowiredTypes()
        {
            _autowiredTypes = new List<AutowiredType>();
            foreach (var type in ModuleDefinition.Types)
            {
                if (!type.IsClass || type.IsValueType || type.IsDelegate()) continue;

                var autoType = new AutowiredType(type);
                foreach (var prop in type.Properties)
                {
                    if ((prop.GetMethod.Attributes & MethodAttributes.Abstract) != 0) continue;

                    CustomAttribute autoAttr = null;
                    foreach (var attr in prop.CustomAttributes)
                    {
                        if (attr.AttributeType.DerivesFrom(Consts.AutowiredAttribute))
                        {
                            if (autoAttr != null) throw new PropertyAutowiredException("multiple property autowired attribute found!");
                            autoAttr = attr;
                        }
                    }
                    if (autoAttr != null)
                    {
                        var autoProp = new AutowiredProperties(prop, autoAttr);
                        if (prop.HasThis) autoType.InstanceProps.Add(autoProp);
                        else autoType.StaticProps.Add(autoProp);
                    }
                }
                if (autoType.HasAutowired) _autowiredTypes.Add(autoType);
            }
        }
    }
}
