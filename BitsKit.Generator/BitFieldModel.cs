using System;
using System.Text;
using Microsoft.CodeAnalysis;

namespace BitsKit.Generator;

/// <summary>
/// A model representing a bit-field to be generated
/// </summary>
internal sealed class BitFieldModel
{
    public string Name { get; private set; } = null!;
    public BitFieldType? FieldType { get; set; }
    public IFieldSymbol BackingField { get; set; } = null!;
    public BackingFieldType BackingFieldType { get; set; }
    public int BitOffset { get; set; }
    public int BitCount { get; set; }
    public BitOrder BitOrder { get; set; }
    public bool ReverseBitOrder { get; }
    public BitFieldModifiers Modifiers { get; }
    public bool IsBoolean { get; }

    public BitFieldModel(AttributeData attributeData, string attributeType)
    {
        switch (attributeType)
        {
            case StringConstants.BitFieldAttributeFullName:
                CreateFromBitFieldAttribute(attributeData);
                break;
            case StringConstants.BooleanFieldAttributeFullName:
                CreateFromBooleanFieldAttribute(attributeData);
                break;
        }

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

        if (string.IsNullOrEmpty(Name))
            FieldType = BitFieldType.Padding;

        if (FieldType == BitFieldType.Boolean)
            IsBoolean = true;
    }

    public void GenerateCSharpSource(StringBuilder sb)
    {
        string accessor = (Modifiers & BitFieldModifiers.AccessorMask) switch
        {
            BitFieldModifiers.Public => "public",
            BitFieldModifiers.Internal => "internal",
            BitFieldModifiers.Private => "private",
            _ => "public",
        };

        // property
        sb.AppendIndentedLine(2,
            GetPropertyTemplate(),
            accessor,
            Modifiers.HasFlag(BitFieldModifiers.Required) ? "required" : "",
            IsBoolean ? BitFieldType.Boolean : FieldType,
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

    private void CreateFromBitFieldAttribute(AttributeData attributeData)
    {
        switch (attributeData.ConstructorArguments.Length)
        {
            case 1: // Padding constructor
                BitCount = (byte)attributeData.ConstructorArguments[0].Value!;
                break;
            case 2: // Integral backed constructor
                Name = (string)attributeData.ConstructorArguments[0].Value!;
                BitCount = (byte)attributeData.ConstructorArguments[1].Value!;
                break;
            case 3: // Memory backed constructor
                Name = (string)attributeData.ConstructorArguments[0].Value!;
                BitCount = (byte)attributeData.ConstructorArguments[1].Value!;
                FieldType = (BitFieldType)attributeData.ConstructorArguments[2].Value!;
                break;
        }
    }

    private void CreateFromBooleanFieldAttribute(AttributeData attributeData)
    {
        if (attributeData.ConstructorArguments.Length != 1)
            return;

        Name = (string)attributeData.ConstructorArguments[0].Value!;
        BitCount = 1;
        FieldType = BitFieldType.Boolean;
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
    private string GetPropertyTemplate()
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
    private string GetGetterTemplate()
    {
        string source = BackingFieldType switch
        {
            BackingFieldType.Integral => "{2}",
            BackingFieldType.Memory => "{2}.Span",
            BackingFieldType.Span => "{2}",
            BackingFieldType.Pointer => "MemoryMarshal.CreateReadOnlySpan(ref {2}[0], {5})",
            _ => throw new NotSupportedException()
        };

        string getter = (BackingFieldType, IsBoolean) switch
        {
            { IsBoolean: false } => StringConstants.IntegralGetterTemplate,
            { BackingFieldType: BackingFieldType.Integral } => StringConstants.BooleanGetterTemplate,
            { BackingFieldType: not BackingFieldType.Integral } => StringConstants.BooleanSpanGetterTemplate,
        };

        return string.Format(getter, source);
    }

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
    private string GetSetterTemplate()
    {
        string source = BackingFieldType switch
        {
            BackingFieldType.Integral => "ref {3}",
            BackingFieldType.Memory => "{3}.Span",
            BackingFieldType.Span => "{3}",
            BackingFieldType.Pointer => "MemoryMarshal.CreateSpan(ref {3}[0], {6})",
            _ => throw new NotSupportedException()
        };

        string setter = (BackingFieldType, IsBoolean) switch
        {
            { IsBoolean: false } => StringConstants.IntegralSetterTemplate,
            { BackingFieldType: BackingFieldType.Integral } => StringConstants.BooleanSetterTemplate,
            { BackingFieldType: not BackingFieldType.Integral } => StringConstants.BooleanSpanSetterTemplate,
        };

        return string.Format(setter, source, FieldType);
    }

    /// <summary>
    /// Determines if this field is ReadOnly based on it's BackingField and Modifiers
    /// </summary>
    private bool IsReadOnly()
    {
        string backingType = BackingField.Type.ToDisplayString();

        return BackingField.IsReadOnly ||
               backingType == "System.ReadOnlySpan<byte>" ||
               backingType == "System.ReadOnlyMemory<byte>" ||
               Modifiers.HasFlag(BitFieldModifiers.ReadOnly);
    }
}
