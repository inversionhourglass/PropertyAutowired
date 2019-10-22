using System.Reflection;
using TestImplAssembly;
using TestUsingAssembly;

[module: TestTypedAutowired(TargetPropertyTypes = new[] { typeof(long), typeof(int) }, PropertyFlags = new[] { BindingFlags.Instance | BindingFlags.Public, BindingFlags.Static | BindingFlags.Public })]
[assembly: TestTypedAutowired(TargetPropertyTypes = new[] { typeof(float), typeof(double) }, PropertyFlags = new[] { BindingFlags.NonPublic, BindingFlags.Static }, ExceptedDeclaringTypes = new[] { typeof(TypedTModel2) })]
[assembly: TestArrayTypedAutowired(TargetPropertyTypes = new[] { typeof(int[]) }, PropertyFlags = new[] { BindingFlags.Instance | BindingFlags.Public, BindingFlags.Static | BindingFlags.Public })]