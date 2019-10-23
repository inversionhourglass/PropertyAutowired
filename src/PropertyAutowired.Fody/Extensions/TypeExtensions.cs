using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace PropertyAutowired.Fody
{
    internal static class TypeExtensions
    {
        public static bool Is(this TypeReference typeRef, string fullName)
        {
            return typeRef.Resolve().FullName == fullName;
        }

        public static bool DerivesFrom(this TypeReference typeRef, string baseClass)
        {
            do
            {
                if ((typeRef = typeRef.Resolve().BaseType)?.FullName == baseClass) return true;
            } while (typeRef != null);
            return false;
        }

        public static bool IsDelegate(this TypeReference typeRef)
        {
            return DerivesFrom(typeRef, Consts.MulticastDelegate);
        }

        public static bool IsArray(this TypeReference typeRef, out TypeReference elementType)
        {
            elementType = null;
            if (!typeRef.IsArray)
                return false;

            elementType = ((ArrayType)typeRef).ElementType;
            return true;
        }

        public static bool IsEnum(this TypeReference typeRef, out TypeReference underlyingType)
        {
            var typeDef = typeRef.Resolve();
            if (typeDef.IsEnum)
            {
                underlyingType = typeDef.Fields.First(f => f.Name == "value__").FieldType;
                return true;
            }

            underlyingType = null;
            return false;
        }

        public static OpCode GetStElemCode(this TypeReference typeRef)
        {
            var typeDef = typeRef.Resolve();
            if (typeDef.IsEnum(out TypeReference underlying))
                return underlying.MetadataType.GetStElemCode();
            if (typeRef.IsValueType)
                return typeRef.MetadataType.GetStElemCode();
            return OpCodes.Stelem_Ref;
        }

        public static OpCode GetStElemCode(this MetadataType type)
        {
            switch (type)
            {
                case MetadataType.Boolean:
                case MetadataType.Int32:
                case MetadataType.UInt32:
                    return OpCodes.Stelem_I4;
                case MetadataType.Byte:
                case MetadataType.SByte:
                    return OpCodes.Stelem_I1;
                case MetadataType.Char:
                case MetadataType.Int16:
                case MetadataType.UInt16:
                    return OpCodes.Stelem_I2;
                case MetadataType.Double:
                    return OpCodes.Stelem_R8;
                case MetadataType.Int64:
                case MetadataType.UInt64:
                    return OpCodes.Stelem_I8;
                case MetadataType.Single:
                    return OpCodes.Stelem_R4;
                default:
                    return OpCodes.Stelem_Ref;
            }
        }

        public static MethodReference RecursionImportPropertySet(this CustomAttribute attribute, ModuleDefinition moduleDef, string propertyName)
        {
            return RecursionImportPropertySet(attribute.AttributeType.Resolve(), moduleDef, propertyName);
        }

        public static MethodReference RecursionImportPropertySet(this TypeDefinition typeDef, ModuleDefinition moduleDef, string propertyName)
        {
            var propertyDef = typeDef.Properties.FirstOrDefault(pd => pd.Name == propertyName);
            if (propertyDef != null) return moduleDef.ImportReference(propertyDef.SetMethod);

            var baseTypeDef = typeDef.BaseType.Resolve();
            if (baseTypeDef.FullName == typeof(object).FullName) throw new PropertyAutowiredException($"can not find property({propertyName}) from {typeDef.FullName}");
            return RecursionImportPropertySet(baseTypeDef, moduleDef, propertyName);
        }

        public static MethodReference RecursionImportMethod(this CustomAttribute attribute, ModuleDefinition moduleDef, string methodName)
        {
            return RecursionImportMethod(attribute.AttributeType.Resolve(), moduleDef, methodName);
        }

        public static MethodReference RecursionImportMethod(this TypeDefinition typeDef, ModuleDefinition moduleDef, string methodName)
        {
            var methodDef = typeDef.Methods.FirstOrDefault(md => md.Name == methodName && !md.HasParameters);
            if (methodDef != null) return moduleDef.ImportReference(methodDef);

            var baseTypeDef = typeDef.BaseType.Resolve();
            if (baseTypeDef.FullName == typeof(object).FullName) throw new PropertyAutowiredException($"can not find method({methodName}) from {typeDef.FullName}");
            return RecursionImportMethod(baseTypeDef, moduleDef, methodName);
        }

        public static VariableDefinition CreateVariable(this MethodBody body, TypeReference variableTypeReference)
        {
            var variable = new VariableDefinition(variableTypeReference);
            body.Variables.Add(variable);
            return variable;
        }

        public static System.Reflection.BindingFlags GetBindingFlags(this PropertyDefinition propertyDef)
        {
            var flags = System.Reflection.BindingFlags.Default;
            flags |= propertyDef.GetMethod.IsPublic ? System.Reflection.BindingFlags.Public : System.Reflection.BindingFlags.NonPublic;
            flags |= propertyDef.HasThis ? System.Reflection.BindingFlags.Instance : System.Reflection.BindingFlags.Static;
            return flags;
        }
    }
}
