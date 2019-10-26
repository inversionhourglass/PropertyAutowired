using PropertyAutowired;
using System;
using System.Reflection;
using TestImplAssembly;
using TestImplAssembly.Logging;
using TestUsingAssembly;

[module: TestTypedAutowired(TargetPropertyTypes = new[] { typeof(int) }, Position = Position.AfterBaseConstructor, PropertyFlags = new[] { PropFlags.Instance | PropFlags.Public, PropFlags.Static | PropFlags.Public })]
//[assembly: TestTypedAutowired(TargetPropertyTypes = new[] { typeof(double) }, PropertyFlags = new[] { Flags.NonPublic, Flags.Static }, ExceptedDeclaringTypes = new[] { typeof(TypedTModel2) })]
[assembly: TestArrayTypedAutowired(TargetPropertyTypes = new[] { typeof(int[]) }, Order =3, PropertyFlags = new[] { PropFlags.Instance | PropFlags.Public, PropFlags.Static | PropFlags.Public })]
[assembly: TypedLoggerAutowired(TargetPropertyTypes = new[] { typeof(ILogger), typeof(DefaultLogger) }, Position = Position.AfterDefaultInit)]
[assembly: TypedGenericAutowired(TargetPropertyTypes = new[] { typeof(Func<Type, string>) })]
//[module: Normal]
//[assembly: Normal(TargetPropertyTypes = new[] { typeof(float), typeof(double) }, PropertyFlags = new[] { Flags.NonPublic, Flags.Static }, ExceptedDeclaringTypes = new[] { typeof(TypedTModel2) })]