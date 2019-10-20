using System;

namespace PropertyAutowired.Fody
{
    public partial class ModuleWeaver
    {
        private void LoadReferences()
        {
            _sysTypeRef = FindType(typeof(Type).FullName).Import(ModuleDefinition);
            _getTypeFromHandleRef = ModuleDefinition.ImportReference(typeof(Type).GetMethod("GetTypeFromHandle", new[] { typeof(RuntimeTypeHandle) }));
        }
    }
}
