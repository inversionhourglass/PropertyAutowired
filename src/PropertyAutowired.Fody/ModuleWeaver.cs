using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil.Rocks;
using System;
using Mono.Collections.Generic;
using CustomAttributeNamedArgument = Mono.Cecil.CustomAttributeNamedArgument;
using MethodAttributes = Mono.Cecil.MethodAttributes;
using MethodBody = Mono.Cecil.Cil.MethodBody;

namespace PropertyAutowired.Fody
{
    public partial class ModuleWeaver : BaseModuleWeaver
    {
        private List<AutowiredType> _autowiredTypes;

        private TypeReference _sysTypeRef;
        private MethodReference _getTypeFromHandleRef;

        public override void Execute()
        {
            LoadReferences();
            FindAutowiredTypes();
            WeaveAutowired();
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
