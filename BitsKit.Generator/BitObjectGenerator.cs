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
                StringConstants.BitObjectAttributeFullName,
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
            .GetAttributes(StringConstants.BitObjectAttributeFullName)
            .Single();

        return new(typeSymbol, typeDeclaration, attribute);
    }

    private static void GenerateSourceCode(SourceProductionContext context, (Compilation _, ImmutableArray<TypeSymbolProcessor> Processors) result)
    {
        if (result.Processors.Length == 0)
            return;

        StringBuilder stringBuilder = new(StringConstants.Header);

        // group the objects by their respective namespace
        var namespaceGroups = result.Processors.GroupBy(x => x.Namespace);

        foreach (var namespaceGroup in namespaceGroups)
        {
            stringBuilder.AppendLine();

            // print the current namespace
            if (namespaceGroup.Key is not null)
                stringBuilder
                    .AppendLine($"namespace {namespaceGroup.Key.Name.ToFullString()}")
                    .AppendLine("{");

            foreach (TypeSymbolProcessor processor in namespaceGroup)
            {
                // evaluate if there are actually any valid fields
                if (processor.EnumerateFields() == 0)
                    continue;

                // check and report any compilation issues
                DiagnosticDescriptor? diagnosticError = processor switch
                {
                    _ when IsNotPartial(processor.TypeDeclaration) => DiagnosticDescriptors.MustBePartial,
                    _ when IsNested(processor.TypeDeclaration) => DiagnosticDescriptors.NestedNotAllowed,
                    _ when HasMissingFieldTypes(processor) => DiagnosticDescriptors.BitFieldTypeNotDefined,
                    _ when HasConflictingAccessors(processor) => DiagnosticDescriptors.ConflictingAccessors,
                    _ when HasConflictingSetters(processor) => DiagnosticDescriptors.ConflictingSetters,
                    _ => null
                };

                if (diagnosticError is not null)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        diagnosticError,
                        processor.TypeDeclaration.Identifier.GetLocation(),
                        processor.TypeSymbol.Name));
                }

                // prevent code generation on error severity issues
                if (diagnosticError is { DefaultSeverity: DiagnosticSeverity.Error })
                    continue;

                processor.GenerateCSharpSource(stringBuilder);
            }

            // remove typesymbol seperator
            stringBuilder.RemoveLastLine();

            // apply closing namespace bracket
            if (namespaceGroup.Key is not null)
                stringBuilder.AppendLine("}");
        }

        context.AddSource("BitsKitGeneratedFields.g.cs", stringBuilder.ToString());
    }

    private static bool IsValidTypeDeclaration(SyntaxNode node, CancellationToken _) =>
        node is ClassDeclarationSyntax or StructDeclarationSyntax or RecordDeclarationSyntax;

    private static bool IsNotPartial(TypeDeclarationSyntax typeDeclaration) =>
        !typeDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword));

    private static bool IsNested(TypeDeclarationSyntax typeDeclaration) =>
        typeDeclaration.Parent is TypeDeclarationSyntax;

    private static bool HasMissingFieldTypes(TypeSymbolProcessor processor) =>
        processor.Fields.Any(f => f.FieldType is null);

    private static bool HasConflictingAccessors(TypeSymbolProcessor processor) =>
        processor.Fields.Any(f => !Enum.IsDefined(typeof(BitFieldModifiers), f.Modifiers & BitFieldModifiers.AccessorMask));

    private static bool HasConflictingSetters(TypeSymbolProcessor processor) =>
        processor.Fields.Any(f => !Enum.IsDefined(typeof(BitFieldModifiers), f.Modifiers & BitFieldModifiers.SetterMask));
}

file class TypeSymbolProcessorComparer : IEqualityComparer<TypeSymbolProcessor?>
{
    public static TypeSymbolProcessorComparer Default { get; } = new();

    public bool Equals(TypeSymbolProcessor? x, TypeSymbolProcessor? y) =>
        SymbolEqualityComparer.Default.Equals(x?.TypeSymbol, y?.TypeSymbol);

    public int GetHashCode(TypeSymbolProcessor? obj) =>
        SymbolEqualityComparer.Default.GetHashCode(obj?.TypeSymbol);
}
