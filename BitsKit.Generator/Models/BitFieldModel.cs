using System;
using System.Text;
using Microsoft.CodeAnalysis;

namespace BitsKit.Generator.Models;

internal abstract class BitFieldModel
{
    public string Name { get; set; } = null!;
    public BitFieldType? FieldType { get; set; }
    public string? ReturnType { get; set; }
    public IFieldSymbol BackingField { get; set; } = null!;
    public BackingFieldType BackingFieldType { get; set; }
    public int BitOffset { get; set; }
    public int BitCount { get; set; }
    public BitOrder BitOrder { get; set; }
    public bool ReverseBitOrder { get; }
    public BitFieldModifiers Modifiers { get; }
    public TypeSymbolProcessor TypeSymbol { get; }

    public BitFieldModel(AttributeData attributeData, TypeSymbolProcessor typeSymbol)
    {
        TypeSymbol = typeSymbol;

        for (int i = 0; i < attributeData.NamedArguments.Length; i++)
        {
            switch (attributeData.NamedArguments[i].Key)
            {
                case "ReverseBitOrder":
                    ReverseBitOrder = (bool)attributeData.NamedArguments[i].Value.Value!;
                    break;
                case "Modifiers":
                    Modifiers = (BitFieldModifiers)attributeData.NamedArguments[i].Value.Value!;
                    break;
            }
        }
    }

    public void GenerateCSharpSource(StringBuilder sb)
    {
        string accessor = (Modifiers & BitFieldModifiers.AccessorMask) switch
        {
            BitFieldModifiers.Public => "public",
            BitFieldModifiers.Private => "private",
            BitFieldModifiers.Protected => "protected",
            BitFieldModifiers.Internal => "internal",
            BitFieldModifiers.ProtectedInternal => "protected internal",
            BitFieldModifiers.PrivateProtected => "private protected",
            _ => "public",
        };

        // property
        sb.AppendIndentedLine(2,
            GetPropertyTemplate(),
            accessor,
            Modifiers.HasFlag(BitFieldModifiers.Required) ? "required" : "",
            ReturnType ?? FieldType.ToString(),
            Name)
          .AppendIndentedLine(2, "{");

        // getter
        {
            sb.AppendIndentedLine(3,
                GetGetterTemplate(),
                SupportsReadOnlyGetter() ? "readonly" : "",
                "get",
                FieldType!.Value.ToIntegralName(),
                BitOrder.ToShortName(),
                BackingField.Name,
                BitOffset,
                BitCount,
                BackingField.FixedSize);
        }

        // setter
        if (!IsReadOnly())
        {
            sb.AppendIndentedLine(3,
                GetSetterTemplate(),
                "",
                Modifiers.HasFlag(BitFieldModifiers.InitOnly) ? "init" : "set",
                FieldType!.Value.ToIntegralName(),
                BitOrder.ToShortName(),
                BackingField.Name,
                BitOffset,
                BitCount,
                BackingField.FixedSize);
        }

        sb.AppendIndentedLine(2, "}")
          .AppendLine();
    }

    /// <summary>
    /// Diagnoses if the field will produce non-compilable or erroneous code
    /// </summary>
    public virtual bool HasCompilationIssues(SourceProductionContext context, TypeSymbolProcessor processor)
    {
        return DiagnosticValidator.HasMissingFieldType(context, this, processor.TypeSymbol.Name) |
               DiagnosticValidator.HasConflictingAccessors(context, this, processor.TypeSymbol.Name) |
               DiagnosticValidator.HasConflictingSetters(context, this, processor.TypeSymbol.Name);
    }

    /// <summary>
    /// Generates a template for the property accessors, type and name
    /// <para>
    /// {0} = Accessor<br/>
    /// {1} = Required modifier<br/>
    /// {2} = <see cref="FieldType"/><br/>
    /// {3} = <see cref="Name"/>
    /// </para> 
    /// </summary>
    protected string GetPropertyTemplate()
    {
        return BackingFieldType switch
        {
            BackingFieldType.Integral or
            BackingFieldType.Memory or
            BackingFieldType.Span => StringConstants.PropertyTemplate,
            BackingFieldType.Pointer => StringConstants.UnsafePropertyTemplate,
            _ => throw new NotSupportedException(),
        };
    }

    /// <summary>
    /// Generates a template for the property getter
    /// <para>
    /// {0} = Getter prefix<br/>
    /// {1} = Getter type<br/>
    /// {2} = BitPrimitives method<br/>
    /// {3} = <see cref="BitOrder"/><br/>
    /// {4} = IntegralName<br/>
    /// {5} = <see cref="BitOffset"/><br/> 
    /// {6} = <see cref="BitCount"/><br/>
    /// {7} = <see cref="BackingField"/>.FixedSize
    /// </para> 
    /// </summary>
    protected abstract string GetGetterTemplate();

    /// <summary>
    /// Generates a template for the property setter
    /// <para>
    /// {0} = Setter prefix<br/>
    /// {1} = Setter type<br/>
    /// {2} = BitPrimitive method<br/>
    /// {3} = <see cref="BitOrder"/><br/>
    /// {4} = IntegralName<br/>
    /// {5} = <see cref="BitOffset"/><br/> 
    /// {6} = <see cref="BitCount"/><br/>
    /// {7} = <see cref="BackingField"/>.FixedSize
    /// </para> 
    /// </summary>
    protected abstract string GetSetterTemplate();

    /// <summary>
    /// Gets the getter's source template based on it's BackingField
    /// </summary>
    protected string GetterSource() => BackingFieldType switch
    {
        BackingFieldType.Integral => "{4}",
        BackingFieldType.Memory => "{4}.Span",
        BackingFieldType.Span => "{4}",
        BackingFieldType.Pointer => "MemoryMarshal.CreateReadOnlySpan(ref {4}[0], {7})",
        _ => throw new NotSupportedException()
    };

    /// <summary>
    /// Gets the setter's source template based on it's BackingField
    /// </summary>
    protected string SetterSource() => BackingFieldType switch
    {
        BackingFieldType.Integral => "ref {4}",
        BackingFieldType.Memory => "{4}.Span",
        BackingFieldType.Span => "{4}",
        BackingFieldType.Pointer => "MemoryMarshal.CreateSpan(ref {4}[0], {7})",
        _ => throw new NotSupportedException()
    };

    /// <summary>
    /// Determines if this field is ReadOnly based on it's BackingField and Modifiers
    /// </summary>
    protected bool IsReadOnly()
    {
        string backingType = BackingField.Type.ToDisplayString();

        return BackingField.IsReadOnly ||
               backingType == "System.ReadOnlySpan<byte>" ||
               backingType == "System.ReadOnlyMemory<byte>" ||
               Modifiers.HasFlag(BitFieldModifiers.ReadOnly);
    }

    /// <summary>
    /// Determines if this property can have a readonly instance member
    /// </summary>
    /// <returns></returns>
    private bool SupportsReadOnlyGetter()
    {
        return TypeSymbol.TypeDeclaration.IsStruct() &&
               BackingFieldType != BackingFieldType.Pointer &&
               !IsReadOnly();
    }
}
