using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;

namespace PropertyAutowired.Fody
{
    public partial class ModuleWeaver
    {
        private void WeaveAutowired()
        {
            foreach (var autoType in _autowiredTypes)
            {
                InstanceAutowired(autoType.TypeDef, autoType.InstanceProps);

                StaticAutowired(autoType.TypeDef, autoType.StaticProps);
            }
        }

        private void InstanceAutowired(TypeDefinition typeDef, List<AutowiredProperties> props)
        {
            if (props.Count == 0) return;

            var ctors = MakeSureInstanceCtorAndGet(typeDef);
            foreach (var ctor in ctors)
            {
                WiredProperty(ctor, props, true);
                ctor.Body.Optimize();
            }
        }

        private void StaticAutowired(TypeDefinition typeDef, List<AutowiredProperties> props)
        {
            if (props.Count == 0) return;

            var ctor = MakeSureStaticCtorAndGet(typeDef);
            WiredProperty(ctor, props, false);
            ctor.Body.Optimize();
        }

        private void WiredProperty(MethodDefinition ctor, List<AutowiredProperties> props, bool isInstance)
        {
            var ins = new Collection<Instruction>();
            foreach (var prop in props)
            {
                if (isInstance) ins.Add(Instruction.Create(OpCodes.Ldarg_0));
                var attr = ctor.Body.CreateVariable(prop.Attribute.AttributeType);
                ins.AddRange(LoadAttributeArgumentIns(prop.Attribute.ConstructorArguments));
                ins.Add(Instruction.Create(OpCodes.Newobj, prop.Attribute.Constructor));
                ins.Add(Instruction.Create(OpCodes.Stloc, attr));
                ins.Add(Instruction.Create(OpCodes.Ldloc, attr));
                ins.AddRange(LoadValueOnStack(_sysTypeRef, ctor.DeclaringType));
                ins.Add(Instruction.Create(OpCodes.Callvirt, prop.Attribute.AttributeType.Resolve().RecursionImportPropertySet(ModuleDefinition, "TargetType")));
                ins.Add(Instruction.Create(OpCodes.Nop));
                if (prop.Attribute.HasProperties)
                {
                    ins.AddRange(LoadAttributePropertyIns(prop.Attribute.AttributeType.Resolve(), prop.Attribute.Properties, attr));
                }
                ins.Add(Instruction.Create(OpCodes.Ldloc, attr));
                ins.Add(Instruction.Create(OpCodes.Callvirt, prop.Attribute.AttributeType.Resolve().RecursionImportMethod(ModuleDefinition, "GetPropertyValue")));
                ins.Add(Instruction.Create(OpCodes.Castclass, prop.PropertyDef.GetMethod.ReturnType));
                var propSetOpCode = isInstance ? OpCodes.Stfld : OpCodes.Stsfld;
                ins.Add(Instruction.Create(propSetOpCode, ctor.DeclaringType.Fields.First(fd => fd.Name == $"<{prop.PropertyDef.Name}>k__BackingField")));
            }
            ctor.Body.Instructions.InsertRange(0, ins);
        }

        private Collection<Instruction> LoadAttributeArgumentIns(Collection<CustomAttributeArgument> arguments)
        {
            var ins = new Collection<Instruction>();
            foreach (var arg in arguments)
            {
                ins.AddRange(LoadValueOnStack(arg.Type, arg.Value));
            }
            return ins;
        }

        private Collection<Instruction> LoadAttributePropertyIns(TypeDefinition attrTypeDef, Collection<CustomAttributeNamedArgument> properties, VariableDefinition attributeDef)
        {
            var ins = new Collection<Instruction>();
            for (var i = 0; i < properties.Count; i++)
            {
                ins.Add(Instruction.Create(OpCodes.Ldloc, attributeDef));
                ins.AddRange(LoadValueOnStack(properties[i].Argument.Type, properties[i].Argument.Value));
                ins.Add(Instruction.Create(OpCodes.Callvirt, attrTypeDef.RecursionImportPropertySet(ModuleDefinition, properties[i].Name)));
                ins.Add(Instruction.Create(OpCodes.Nop));
            }

            return ins;
        }
    }
}
