using Mono.Cecil;

namespace PropertyAutowired.Fody
{
    public static class TypeExtensions
    {
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
    }
}
