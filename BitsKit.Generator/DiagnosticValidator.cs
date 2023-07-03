using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;

namespace BitsKit.Generator;

internal static class DiagnosticValidator
{
    /// <summary>
    /// Adds a diagnostic to the compilation and returns true
    /// if compilation will fail because of it
    /// </summary>
    public static bool ReportDiagnostic(SourceProductionContext context, DiagnosticDescriptor descriptor, Location location, params object?[]? messageArgs)
    {
        context.ReportDiagnostic(Diagnostic.Create(descriptor, location, messageArgs));

        return descriptor is { DefaultSeverity: DiagnosticSeverity.Error };
    }

    public static bool IsNotPartial(SourceProductionContext context, TypeDeclarationSyntax typeDeclaration, string typeName)
    {
        return !typeDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword))
            && ReportDiagnostic(
                context,
                DiagnosticDescriptors.MustBePartial,
                typeDeclaration.GetLocation(),
                typeName);
    }

    public static bool IsNested(SourceProductionContext context, TypeDeclarationSyntax typeDeclaration, string typeName)
    {
        return typeDeclaration.Parent is TypeDeclarationSyntax
            && ReportDiagnostic(
                context,
                DiagnosticDescriptors.NestedNotAllowed,
                typeDeclaration.GetLocation(),
                typeName);
    }

    public static bool HasMissingFieldType(SourceProductionContext context, BitFieldModel bitField, string typeName)
    {
        return bitField.FieldType is null
            && ReportDiagnostic(
                context,
                DiagnosticDescriptors.FieldTypeNotDefined,
                bitField.BackingField.Locations[0],
                typeName,
                bitField.Name);
    }

    public static bool HasConflictingAccessors(SourceProductionContext context, BitFieldModel bitField, string typeName)
    {
        int modifiers = (int)(bitField.Modifiers & BitFieldModifiers.AccessorMask);

        return (modifiers & (modifiers - 1)) != 0
            && ReportDiagnostic(
                context,
                DiagnosticDescriptors.ConflictingAccessors,
                bitField.BackingField.Locations[0],
                typeName,
                bitField.Name);
    }

    public static bool HasConflictingSetters(SourceProductionContext context, BitFieldModel bitField, string typeName)
    {
        int modifiers = (int)(bitField.Modifiers & BitFieldModifiers.SetterMask);

        return (modifiers & (modifiers - 1)) != 0
            && ReportDiagnostic(
                context,
                DiagnosticDescriptors.ConflictingSetters,
                bitField.BackingField.Locations[0],
                typeName,
                bitField.Name);
    }
}
