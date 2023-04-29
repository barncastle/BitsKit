using Microsoft.CodeAnalysis;

namespace BitsKit.Generator;

internal static class DiagnosticDescriptors
{
    const string Category = "BitsKit.Generator";

    public static readonly DiagnosticDescriptor MustBePartial = new(
       id: "BITSKIT001",
       title: "BitsKit object must be partial",
       messageFormat: "'{0}' must be partial",
       category: Category,
       defaultSeverity: DiagnosticSeverity.Error,
       isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor NestedNotAllowed = new(
        id: "BITSKIT002",
        title: "BitsKit object must not be a nested type",
        messageFormat: "'{0}' must not be a nested type",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor BitFieldTypeNotDefined = new(
        id: "BITSKIT003",
        title: "Cannot infer FieldType",
        messageFormat: "'{0}' contains one or more fields without an inferable BitFieldType",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor ConflictingAccessors = new(
        id: "BITSKIT004",
        title: "Conflicting accessability modifiers",
        messageFormat: "'{0}' contains one or more fields with conflicting accessors",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor ConflictingSetters = new(
        id: "BITSKIT005",
        title: "Conflicting setter modifiers",
        messageFormat: "'{0}' contains one or more fields with conflicting setter modifiers",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);
}
