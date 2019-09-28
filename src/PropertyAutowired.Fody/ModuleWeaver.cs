using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.Linq;

namespace PropertyAutowired.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        private Dictionary<int, OpCode> _ldcI4Maps;
        private List<AutowiredType> _autowiredTypes;

        private TypeDefinition _sysTypeDef;

        public override void Execute()
        {
            _sysTypeDef = FindType("System.Type");
            FindAutowiredTypes();
            Autowired();
        }

        private void LoadOpMapping()
        {
            _ldcI4Maps = new Dictionary<int, OpCode>
            {
                { 0, OpCodes.Ldc_I4_0 },
                { 1, OpCodes.Ldc_I4_1 },
                { 2, OpCodes.Ldc_I4_2 },
                { 3, OpCodes.Ldc_I4_3 },
                { 4, OpCodes.Ldc_I4_4 },
                { 5, OpCodes.Ldc_I4_5 },
                { 6, OpCodes.Ldc_I4_6 },
                { 7, OpCodes.Ldc_I4_7 },
                { 8, OpCodes.Ldc_I4_8 }
            };
        }

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

        private void Autowired()
        {
            foreach (var autoType in _autowiredTypes)
            {
                InstanceAutowired(autoType.TypeDef, autoType.InstanceProps);

                StaticAutowired(autoType.TypeDef, autoType.StaticProps);
            }
        }

        private void InstanceAutowired(TypeDefinition typeDef, List<AutowiredProperties> props)
        {
            var ctors = MakeSureInstanceCtorAndGet(typeDef);
            foreach (var ctor in ctors)
            {
                PropertyAutowired(ctor, props);
            }
        }

        private void StaticAutowired(TypeDefinition typeDef, List<AutowiredProperties> props)
        {
            var ctor = MakeSureStaticCtorAndGet(typeDef);
            PropertyAutowired(ctor, props);
        }

        private void PropertyAutowired(MethodDefinition ctor, List<AutowiredProperties> props)
        {
            foreach (var prop in props)
            {
                ctor.Body.Instructions.InsertRange(0, ArgumentInstruction(prop.));
            }
            //CustomAttributeArgument
            //ctor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_0, props[0].Attribute.ConstructorArguments[))
        }

        private List<Instruction> ArgumentInstruction(CustomAttributeArgument argument)
        {
            var argTypeDef = argument.Type.Resolve();
            var list = new List<Instruction>();
            if (argTypeDef == TypeSystem.BooleanDefinition)
            {
                list.Add(Instruction.Create((bool)argument.Value ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0));
            }
            else if(argTypeDef == TypeSystem.CharDefinition)
            {
                list.Add(Instruction.Create(OpCodes.Ldc_I4_S, (char)argument.Value));
            }
            else if(argTypeDef == TypeSystem.ByteDefinition)
            {
                var value = (byte)argument.Value;
                list.Add(value >> 4 == 0
                    ? Instruction.Create(_ldcI4Maps[value])
                    : Instruction.Create(OpCodes.Ldc_I4_S, value));
            }
            else if (argTypeDef == TypeSystem.SByteDefinition)
            {
                var value = (sbyte)argument.Value;
                list.Add(value >> 4 == 0
                    ? Instruction.Create(_ldcI4Maps[value])
                    : Instruction.Create(OpCodes.Ldc_I4_S, value));
            }
            else if (argTypeDef == TypeSystem.Int16Definition)
            {
                var value = (short)argument.Value;
                list.Add(value >> 4 == 0
                    ? Instruction.Create(_ldcI4Maps[value])
                    : Instruction.Create(OpCodes.Ldc_I4_S, value));
            }
            else if (argTypeDef == TypeSystem.UInt16Definition)
            {
                var value = (ushort)argument.Value;
                list.Add(value >> 4 == 0
                    ? Instruction.Create(_ldcI4Maps[value])
                    : Instruction.Create(OpCodes.Ldc_I4_S, value));
            }
            else if (argTypeDef == TypeSystem.Int32Definition)
            {
                var value = (int)argument.Value;
                list.Add(value >> 4 == 0
                    ? Instruction.Create(_ldcI4Maps[value])
                    : Instruction.Create(OpCodes.Ldc_I4_S, value));
            }
            else if (argTypeDef == TypeSystem.UInt32Definition)
            {
                var value = (uint)argument.Value;
                list.Add(value >> 4 == 0
                    ? Instruction.Create(_ldcI4Maps[(int)value])
                    : Instruction.Create(OpCodes.Ldc_I4_S, value));
            }
            else if (argTypeDef == TypeSystem.Int64Definition)
            {
                var value = (long)argument.Value;
                if (value >> 4 == 0)
                {
                    list.Add(Instruction.Create(_ldcI4Maps[(int)value]));
                    list.Add(Instruction.Create(OpCodes.Conv_I8));
                }
                else
                {
                    Instruction.Create(OpCodes.Ldc_I8, value);
                }
            }
            else if (argTypeDef == TypeSystem.UInt64Definition)
            {
                var value = (ulong)argument.Value;
                if (value >> 4 == 0)
                {
                    list.Add(Instruction.Create(_ldcI4Maps[(int)value]));
                    list.Add(Instruction.Create(OpCodes.Conv_I8));
                }
                else
                {
                    Instruction.Create(OpCodes.Ldc_I8, value);
                }
            }
            else if (argTypeDef == TypeSystem.SingleDefinition)
            {
                list.Add(Instruction.Create(OpCodes.Ldc_R4, (float) argument.Value));
            }
            else if (argTypeDef == TypeSystem.DoubleDefinition)
            {
                list.Add(Instruction.Create(OpCodes.Ldc_R8, (double) argument.Value));
            }
            else if (argTypeDef == TypeSystem.StringDefinition)
            {
                list.Add(Instruction.Create(OpCodes.Ldstr, (string) argument.Value));
            }
            else if (argTypeDef == _sysTypeDef)
            {
                Instruction.Create(OpCodes.Ldtoken, (TypeReference) argument.Value);
            }
            // else if enum
            // else if array

            if (list.Count > 0) list.Add(Instruction.Create(OpCodes.Ldarg_0));
            return list;
        }

        private List<MethodDefinition> MakeSureInstanceCtorAndGet(TypeDefinition typeDef)
        {
            var ctors = typeDef.Methods.Where(m => m.IsConstructor && !m.IsStatic && !m.Body.Instructions.Any(ins => ins.OpCode.Code == Code.Call && ins.Operand is MethodDefinition md && md.IsConstructor && md.DeclaringType == typeDef)).ToList();
            if(ctors.Count == 0)
            {
                ctors.Add(GenerateEmptyCtor(typeDef));
            }
            return ctors;
        }

        private MethodDefinition GenerateEmptyCtor(TypeDefinition typeDef)
        {
            var visibility = typeDef.IsAbstract ? MethodAttributes.Family : MethodAttributes.Public;
            var methodAttributes = visibility | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
            var method = new MethodDefinition(".ctor", methodAttributes, TypeSystem.VoidReference);
            method.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_0));
            var methodReference = new MethodReference(".ctor", TypeSystem.VoidReference, typeDef.BaseType) { HasThis = true };
            method.Body.Instructions.Add(Instruction.Create(OpCodes.Call, methodReference));
            method.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));
            typeDef.Methods.Add(method);
            return default;
        }

        private MethodDefinition MakeSureStaticCtorAndGet(TypeDefinition typeDef)
        {
            return default;
        }

        public override IEnumerable<string> GetAssembliesForScanning()
        {
            yield return "netstandard";
            yield return "mscorlib";
            yield return "System";
            yield return "System.Runtime";
            yield return "System.Core";
        }
    }
}
