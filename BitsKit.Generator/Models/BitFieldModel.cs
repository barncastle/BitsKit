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

    public BitFieldModel(AttributeData attributeData)
    {
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
    /// {0} = BitPrimitives method<br/>
    /// {1} = <see cref="BitOrder"/><br/>
    /// {2} = <see cref="BackingField"/><br/>
    /// {3} = <see cref="BitOffset"/><br/> 
    /// {4} = <see cref="BitCount"/><br/>
    /// {5} = <see cref="BackingField"/>.FixedSize
    /// </para> 
    /// </summary>
    protected abstract string GetGetterTemplate();

    /// <summary>
    /// Generates a template for the property setter
    /// <para>
    /// {0} = Setter type<br/>
    /// {1} = BitPrimitive method<br/>
    /// {2} = <see cref="BitOrder"/><br/>
    /// {3} = <see cref="BackingField"/><br/>
    /// {4} = <see cref="BitOffset"/><br/> 
    /// {5} = <see cref="BitCount"/><br/>
    /// {6} = <see cref="BackingField"/>.FixedSize
    /// </para> 
    /// </summary>
    protected abstract string GetSetterTemplate();

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
    /// Gets the getter's source template based on it's BackingField
    /// </summary>
    protected string GetterSource() => BackingFieldType switch
    {
        BackingFieldType.Integral => "{2}",
        BackingFieldType.Memory => "{2}.Span",
        BackingFieldType.Span => "{2}",
        BackingFieldType.Pointer => "MemoryMarshal.CreateReadOnlySpan(ref {2}[0], {5})",
        _ => throw new NotSupportedException()
    };

    /// <summary>
    /// Gets the setter's source template based on it's BackingField
    /// </summary>
    protected string SetterSource() => BackingFieldType switch
    {
        BackingFieldType.Integral => "ref {3}",
        BackingFieldType.Memory => "{3}.Span",
        BackingFieldType.Span => "{3}",
        BackingFieldType.Pointer => "MemoryMarshal.CreateSpan(ref {3}[0], {6})",
        _ => throw new NotSupportedException()
    };
}
