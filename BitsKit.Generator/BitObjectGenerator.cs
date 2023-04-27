using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BitsKit.Generator;

[Generator(LanguageNames.CSharp)]
public sealed class BitObjectGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<TypeSymbolProcessor> typeDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "BitsKit.BitFields.BitObjectAttribute",
                predicate: IsValidTypeDeclaration,
                transform: ProcessSyntaxNode)
            .WithComparer(TypeSymbolProcessorComparer.Default)
            .Where(x => x is not null)!;

        IncrementalValueProvider<(Compilation, ImmutableArray<TypeSymbolProcessor>)> model = context
            .CompilationProvider
            .Combine(typeDeclarations.Collect());

        context.RegisterSourceOutput(model, GenerateSourceCode);
    }

    private static TypeSymbolProcessor? ProcessSyntaxNode(GeneratorAttributeSyntaxContext syntaxContext, CancellationToken token)
    {
        if (syntaxContext.TargetNode is not TypeDeclarationSyntax typeDeclaration)
            return null;

        ISymbol? symbol = syntaxContext.SemanticModel.GetDeclaredSymbol(typeDeclaration, token);

        if (symbol is not INamedTypeSymbol typeSymbol)
            return null;

        AttributeData attribute = typeSymbol
            .GetAttributes("BitsKit.BitFields.BitObjectAttribute")
            .Single();

        return new(typeSymbol, typeDeclaration, attribute);
    }

    private static void GenerateSourceCode(SourceProductionContext context, (Compilation _, ImmutableArray<TypeSymbolProcessor> Processors) result)
    {
        return;
    }

    private static bool IsValidTypeDeclaration(SyntaxNode node, CancellationToken _) =>
        node is ClassDeclarationSyntax or StructDeclarationSyntax or RecordDeclarationSyntax;
}

file class TypeSymbolProcessorComparer : IEqualityComparer<TypeSymbolProcessor?>
{
    public static TypeSymbolProcessorComparer Default { get; } = new();

    public bool Equals(TypeSymbolProcessor? x, TypeSymbolProcessor? y) =>
        SymbolEqualityComparer.Default.Equals(x?.TypeSymbol, y?.TypeSymbol);

    public int GetHashCode(TypeSymbolProcessor? obj) =>
        SymbolEqualityComparer.Default.GetHashCode(obj?.TypeSymbol);
}
