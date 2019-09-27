using Fody;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PropertyAutowired.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        public override void Execute()
        {
            var tpa = from t in ModuleDefinition.Types
            from p in t.Properties
            from a in p.CustomAttributes
            where a.AttributeType.Resolve().BaseType.FullName == "PropertyAutowired.AutowiredAttribute"
            select (type: t, prop: p, attr: a);
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
