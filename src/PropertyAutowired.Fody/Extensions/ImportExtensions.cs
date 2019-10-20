using Mono.Cecil;

namespace PropertyAutowired.Fody
{
    internal static class ImportExtensions
    {
        public static TypeReference Import(this TypeDefinition typeDef, ModuleDefinition moduleDef) => moduleDef.ImportReference(typeDef);

        public static FieldReference Import(this FieldDefinition fieldDef, ModuleDefinition moduleDef) => moduleDef.ImportReference(fieldDef);

        public static MethodReference Import(this MethodDefinition methodDef, ModuleDefinition moduleDef) => moduleDef.ImportReference(methodDef);
    }
}
