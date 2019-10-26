using PropertyAutowired;
using System;
using TestImplAssembly;
using TestImplAssembly.Logging;

[module: TestTypedAutowired(TargetPropertyTypes = new[] { typeof(int) }, Position = Position.AfterBaseConstructor, PropertyFlags = new[] { PropFlags.Public })]
[assembly: TestArrayTypedAutowired(TargetPropertyTypes = new[] { typeof(int[]) }, Order = 3, PropertyFlags = new[] { PropFlags.Public })]
[assembly: TypedLoggerAutowired(TargetPropertyTypes = new[] { typeof(ILogger), typeof(DefaultLogger) }, Position = Position.AfterDefaultInit)]
[assembly: TypedGenericAutowired(TargetPropertyTypes = new[] { typeof(Func<Type, string>) })]
