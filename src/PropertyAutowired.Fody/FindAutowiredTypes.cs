﻿using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace PropertyAutowired.Fody
{
    public partial class ModuleWeaver
    {
        private void FindAutowiredTypes()
        {
            _autowiredTypes = new List<AutowiredType>();
            var attrs = ModuleDefinition.CustomAttributes.Concat(ModuleDefinition.Assembly.CustomAttributes);
            var autowiredTypes = new Dictionary<string, TypedAutowiredDecorator>();
            foreach (var attr in attrs)
            {
                if (!attr.AttributeType.DerivesFrom(Consts.TypedAutowiredAttribute)) continue;
                if (!attr.HasProperties) continue;
                var decorator = new TypedAutowiredDecorator(attr);
                LogInfo($"TypedAutowiredAttribute found: {attr.AttributeType.FullName}");
                if (!autowiredTypes.AddRangeUnique(decorator.TargetPropertyTypes, t => new KeyValuePair<string, TypedAutowiredDecorator>(t, decorator), out var duplicateType))
                    throw new PropertyAutowiredException($"duplicate {Consts.TypedAutowiredAttribute}.{Consts.TypedAutowiredAttribute_TargetPropertyTypes} found: {duplicateType}");
            }
            foreach (var type in ModuleDefinition.Types)
            {
                if (!type.IsClass || type.IsValueType || type.IsDelegate()) continue;

                var typeIgnored = type.CustomAttributes.Any(ca => ca.AttributeType.Is(Consts.IgnoreAutowiredAttribute));
                var autoType = new AutowiredType(type);
                foreach (var prop in type.Properties)
                {
                    if ((prop.GetMethod.Attributes & MethodAttributes.Abstract) != 0) continue;
                    TypedAutowiredDecorator decorator = null;
                    var isTypedAutowired = !typeIgnored && autowiredTypes.TryGetValue(prop.PropertyType.FullName, out decorator);
                    if (isTypedAutowired)
                    {
                        var propBindingFlags = prop.GetPropertyFlags();
                        var flagsCheck = false;
                        foreach (var flags in decorator.PropertyFlags)
                        {
                            flagsCheck |= (propBindingFlags & flags) == flags;
                            if (flagsCheck) break;
                        }
                        isTypedAutowired &= flagsCheck;
                        if (decorator.ExceptedDeclaringTypes != null)
                        {
                            isTypedAutowired &= !decorator.ExceptedDeclaringTypes.Contains(type.FullName);
                        }
                    }
                    CustomAttribute autoAttr = null;
                    foreach (var attr in prop.CustomAttributes)
                    {
                        if (isTypedAutowired && attr.AttributeType.Is(Consts.IgnoreAutowiredAttribute)) isTypedAutowired = false;
                        if (attr.AttributeType.DerivesFrom(Consts.AutowiredAttribute))
                        {
                            if (autoAttr != null) throw new PropertyAutowiredException("multiple property autowired attribute found!");
                            autoAttr = attr;
                        }
                    }

                    var ways = autoAttr != null ? AutowiredWays.Property : (isTypedAutowired ? AutowiredWays.Typed : (AutowiredWays?)null);
                    if (ways.HasValue)
                    {
                        var autoProp = new AutowiredProperties(prop, autoAttr ?? decorator.Attribute, ways.Value);
                        if (prop.HasThis)
                        {
                            autoType.InstanceProps.Add(autoProp);
                        }
                        else
                        {
                            autoType.StaticProps.Add(autoProp);
                        }
                    }
                }
                if (autoType.HasAutowired)
                {
                    autoType.Sort();
                    autoType.Log(LogInfo);
                    _autowiredTypes.Add(autoType);
                }
            }
        }
    }
}
