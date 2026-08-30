using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Ling.FluentValidation.Analyzers.Infrastructure;

internal static class ImplicativeTypes
{
    public static readonly ImmutableDictionary<SpecialType, ImmutableArray<SpecialType>> Special =
        new Dictionary<SpecialType, ImmutableArray<SpecialType>>()
        {
            [SpecialType.System_SByte] = [
                SpecialType.System_Int16,
                SpecialType.System_Int32,
                SpecialType.System_Int64,
                SpecialType.System_Single,
                SpecialType.System_Double,
                SpecialType.System_Decimal
            ],
            [SpecialType.System_Byte] = [
                SpecialType.System_Int16,
                SpecialType.System_UInt16,
                SpecialType.System_Int32,
                SpecialType.System_UInt32,
                SpecialType.System_Int64,
                SpecialType.System_UInt64,
                SpecialType.System_Single,
                SpecialType.System_Double,
                SpecialType.System_Decimal
            ],
            [SpecialType.System_Int16] = [
                SpecialType.System_Int32,
                SpecialType.System_Int64,
                SpecialType.System_Single,
                SpecialType.System_Double,
                SpecialType.System_Decimal
            ],
            [SpecialType.System_UInt16] = [
                SpecialType.System_Int32,
                SpecialType.System_UInt32,
                SpecialType.System_Int64,
                SpecialType.System_UInt64,
                SpecialType.System_Single,
                SpecialType.System_Double,
                SpecialType.System_Decimal
            ],
            [SpecialType.System_Int32] = [
                SpecialType.System_Int64,
                SpecialType.System_Single,
                SpecialType.System_Double,
                SpecialType.System_Decimal
            ],
            [SpecialType.System_UInt32] = [
                SpecialType.System_Int64,
                SpecialType.System_UInt64,
                SpecialType.System_Single,
                SpecialType.System_Double,
                SpecialType.System_Decimal
            ],
            [SpecialType.System_Int64] = [
                SpecialType.System_Single,
                SpecialType.System_Double,
                SpecialType.System_Decimal
            ],
            [SpecialType.System_UInt64] = [
                SpecialType.System_Single,
                SpecialType.System_Double,
                SpecialType.System_Decimal
            ],
            [SpecialType.System_Char] = [
                SpecialType.System_UInt16,
                SpecialType.System_Int32,
                SpecialType.System_UInt32,
                SpecialType.System_Int64,
                SpecialType.System_UInt64,
                SpecialType.System_Single,
                SpecialType.System_Double,
                SpecialType.System_Decimal
            ],
            [SpecialType.System_Single] = [
                SpecialType.System_Double
            ],
        }.ToImmutableDictionary();
}
