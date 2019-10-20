﻿using System;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace PropertyAutowired.Fody
{
    // refer: https://github.com/vescon/MethodBoundaryAspect.Fody/blob/master/src/MethodBoundaryAspect.Fody/InstructionBlockCreator.cs#L313
    public partial class ModuleWeaver
    {
        private IList<Instruction> LoadValueOnStack(TypeReference parameterType, object value)
        {
            if (parameterType.IsArray(out var elementType) && value is CustomAttributeArgument[] args)
            {
                var createArrayInstructions = new List<Instruction>
                {
                    Instruction.Create(OpCodes.Ldc_I4, args.Length),
                    Instruction.Create(OpCodes.Newarr, elementType)
                };

                var stelem = elementType.GetStElemCode();
                for (var i = 0; i < args.Length; ++i)
                {
                    createArrayInstructions.Add(Instruction.Create(OpCodes.Dup));
                    createArrayInstructions.Add(Instruction.Create(OpCodes.Ldc_I4, i));
                    createArrayInstructions.AddRange(LoadValueOnStack(elementType, args[i].Value));
                    createArrayInstructions.Add(Instruction.Create(stelem));
                }

                return createArrayInstructions;
            }

            if (parameterType.IsEnum(out var enumUnderlyingType)) return new List<Instruction>(LoadPrimitiveConstOnStack(enumUnderlyingType.MetadataType, value));

            if (parameterType.IsPrimitive || parameterType.FullName == typeof(string).FullName) return new List<Instruction>(LoadPrimitiveConstOnStack(parameterType.MetadataType, value));

            if (value is TypeReference typeRefValue)
            {
                return new List<Instruction>
                {
                    Instruction.Create(OpCodes.Ldtoken, typeRefValue),
                    Instruction.Create(OpCodes.Call, _getTypeFromHandleRef)
                };
            }

            if (parameterType.FullName == typeof(object).FullName && value is CustomAttributeArgument arg)
            {
                var valueType = arg.Type;
                if (arg.Value is TypeReference)
                    valueType = _sysTypeRef;
                bool isEnum = valueType.IsEnum(out _);
                var instructions = LoadValueOnStack(valueType, arg.Value);
                if (valueType.IsValueType || (!valueType.IsArray && isEnum))
                    instructions.Add(Instruction.Create(OpCodes.Box, valueType));
                return instructions;
            }

            throw new NotSupportedException("Parametertype: " + parameterType);
        }

        private IEnumerable<Instruction> LoadPrimitiveConstOnStack(MetadataType type, object value)
        {
            switch (type)
            {
                case MetadataType.String when (value == null):
                    return new[] { Instruction.Create(OpCodes.Ldnull) };
                case MetadataType.String:
                    return new[] { Instruction.Create(OpCodes.Ldstr, (string)value) };
                case MetadataType.Char:
                    return new[] { Instruction.Create(OpCodes.Ldc_I4, (int)(char)value) };
                case MetadataType.Byte:
                    return new[] { Instruction.Create(OpCodes.Ldc_I4, (int)(byte)value) };
                case MetadataType.SByte:
                    return new[] { Instruction.Create(OpCodes.Ldc_I4, (int)(sbyte)value) };
                case MetadataType.Int16:
                    return new[] { Instruction.Create(OpCodes.Ldc_I4, (int)(short)value) };
                case MetadataType.UInt16:
                    return new[] { Instruction.Create(OpCodes.Ldc_I4, (int)(ushort)value) };
                case MetadataType.Int32:
                    return new[] { Instruction.Create(OpCodes.Ldc_I4, (int)value) };
                case MetadataType.UInt32:
                    return new[] { Instruction.Create(OpCodes.Ldc_I4, (int)(uint)value) };
                case MetadataType.Int64:
                    long longVal = (long)value;
                    if (longVal <= int.MaxValue && longVal >= int.MinValue)
                        return new[]
                        {
                            Instruction.Create(OpCodes.Ldc_I4, (int)longVal),
                            Instruction.Create(OpCodes.Conv_I8)
                        };
                    return new[] { Instruction.Create(OpCodes.Ldc_I8, longVal) };
                case MetadataType.UInt64:
                    ulong ulongVal = (ulong)value;
                    if (ulongVal <= int.MaxValue)
                        return new[]
                        {
                            Instruction.Create(OpCodes.Ldc_I4, (int)ulongVal),
                            Instruction.Create(OpCodes.Conv_I8)
                        };
                    return new[] { Instruction.Create(OpCodes.Ldc_I8, (long)ulongVal) };
                case MetadataType.Boolean:
                    return new[] { Instruction.Create(OpCodes.Ldc_I4, (bool)value ? 1 : 0) };
                case MetadataType.Single:
                    return new[] { Instruction.Create(OpCodes.Ldc_R4, (float)value) };
                case MetadataType.Double:
                    return new[] { Instruction.Create(OpCodes.Ldc_R8, (double)value) };
            }

            throw new NotSupportedException("Not a supported primitve parameter type: " + type);
        }
    }
}
