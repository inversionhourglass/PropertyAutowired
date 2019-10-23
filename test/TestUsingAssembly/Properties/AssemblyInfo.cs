using System;
using System.Reflection;
using TestImplAssembly;
using TestImplAssembly.Logging;
using TestUsingAssembly;

[module: TestTypedAutowired(TargetPropertyTypes = new[] { typeof(int) }, PropertyFlags = new[] { BindingFlags.Instance | BindingFlags.Public, BindingFlags.Static | BindingFlags.Public })]
//[assembly: TestTypedAutowired(TargetPropertyTypes = new[] { typeof(double) }, PropertyFlags = new[] { BindingFlags.NonPublic, BindingFlags.Static }, ExceptedDeclaringTypes = new[] { typeof(TypedTModel2) })]
[assembly: TestArrayTypedAutowired(TargetPropertyTypes = new[] { typeof(int[]) }, PropertyFlags = new[] { BindingFlags.Instance | BindingFlags.Public, BindingFlags.Static | BindingFlags.Public })]
[assembly: TypedLoggerAutowired(TargetPropertyTypes = new[] { typeof(ILogger), typeof(DefaultLogger) })]
[assembly: TypedGenericAutowired(TargetPropertyTypes = new[] { typeof(Func<Type, string>) })]
//[module: Normal]
//[assembly: Normal(TargetPropertyTypes = new[] { typeof(float), typeof(double) }, PropertyFlags = new[] { BindingFlags.NonPublic, BindingFlags.Static }, ExceptedDeclaringTypes = new[] { typeof(TypedTModel2) })]