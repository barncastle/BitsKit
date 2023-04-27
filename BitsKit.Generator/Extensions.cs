using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace BitsKit.Generator;

internal static class Extensions
{
    public static IEnumerable<AttributeData> GetAttributes(this ISymbol symbol, string fullName)
    {
        return symbol.GetAttributes().Where(x => x.AttributeClass?.ToDisplayString() == fullName);
    }
}
