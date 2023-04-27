using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using System.Text;

namespace BitsKit.Generator;

internal sealed class TypeSymbolProcessor
{
    public INamedTypeSymbol TypeSymbol => _typeSymbol;
    public TypeDeclarationSyntax TypeDeclaration => _typeDeclaration;
    public BaseNamespaceDeclarationSyntax? Namespace => _typeDeclaration.Parent as BaseNamespaceDeclarationSyntax;

    private readonly INamedTypeSymbol _typeSymbol;
    private readonly TypeDeclarationSyntax _typeDeclaration;
    private readonly BitOrder _defaultBitOrder;

    public TypeSymbolProcessor(INamedTypeSymbol typeSymbol, TypeDeclarationSyntax typeDeclaration, AttributeData attribute)
    {
        _typeSymbol = typeSymbol;
        _typeDeclaration = typeDeclaration;
        _defaultBitOrder = (BitOrder)attribute.ConstructorArguments[0].Value!;
    }
}
