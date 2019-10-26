using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace PropertyAutowired.Fody
{
    public partial class ModuleWeaver
    {
        private List<MethodDefinition> MakeSureInstanceCtorAndGet(TypeDefinition typeDef)
        {
            var ctors = typeDef.Methods.Where(m => m.IsConstructor && !m.IsStatic && !m.Body.Instructions.Any(ins => ins.OpCode.Code == Code.Call && ins.Operand is MethodDefinition md && md.IsConstructor && md.DeclaringType == typeDef)).ToList();
            if (ctors.Count == 0)
            {
                ctors.Add(GenerateEmptyCtor(typeDef));
            }
            return ctors;
        }

        // refer: https://github.com/Fody/EmptyConstructor/blob/master/EmptyConstructor.Fody/ModuleWeaver.cs#L117
        private MethodDefinition GenerateEmptyCtor(TypeDefinition typeDef)
        {
            var visibility = typeDef.IsAbstract ? MethodAttributes.Family : MethodAttributes.Public;
            var methodAttributes = visibility | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
            var ctor = new MethodDefinition(".ctor", methodAttributes, TypeSystem.VoidReference);
            ctor.Body.Instructions.Add(Instruction.Create(OpCodes.Ldarg_0));
            var methodReference = new MethodReference(".ctor", TypeSystem.VoidReference, typeDef.BaseType) { HasThis = true };
            ctor.Body.Instructions.Add(Instruction.Create(OpCodes.Call, methodReference));
            ctor.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));
            typeDef.Methods.Add(ctor);
            LogInfo($"generated empty instance constructor for {typeDef}");
            return ctor;
        }

        private MethodDefinition MakeSureStaticCtorAndGet(TypeDefinition typeDef)
        {
            var staticCtor = typeDef.Methods.FirstOrDefault(md => md.IsConstructor && md.IsStatic);
            if (staticCtor == null)
            {
                staticCtor = GenerateStaticCtor(typeDef);
            }

            return staticCtor;
        }

        private MethodDefinition GenerateStaticCtor(TypeDefinition typeDef)
        {
            var methodAttributes = MethodAttributes.Private | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName | MethodAttributes.Static;
            var staticCtor = new MethodDefinition(".cctor", methodAttributes, TypeSystem.VoidReference);
            staticCtor.Body.Instructions.Add(Instruction.Create(OpCodes.Nop));
            staticCtor.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));
            typeDef.Methods.Add(staticCtor);
            LogInfo($"generated empty static constructor for {typeDef}");
            return staticCtor;
        }
    }
}
