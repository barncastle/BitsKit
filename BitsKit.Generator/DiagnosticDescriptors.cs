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

    public static readonly DiagnosticDescriptor FieldTypeNotDefined = new(
        id: "BITSKIT003",
        title: "Cannot infer FieldType",
        messageFormat: "'{0}.{1}' FieldType cannot be inferred",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor ConflictingAccessors = new(
        id: "BITSKIT004",
        title: "Conflicting accessability modifiers",
        messageFormat: "'{0}.{1}' has conflicting accessor modifiers",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor ConflictingSetters = new(
        id: "BITSKIT005",
        title: "Conflicting setter modifiers",
        messageFormat: "'{0}.{1}' has conflicting setter modifiers",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public static readonly DiagnosticDescriptor EnumTypeExpected = new(
        id: "BITSKIT006",
        title: "Enum type argument expected",
        messageFormat: "'{0}.{1}' type argument is not an Enum",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
}
