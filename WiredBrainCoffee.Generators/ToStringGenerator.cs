﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WiredBrainCoffee.Generators;

[Generator]
public class ToStringGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var classes = context.SyntaxProvider.CreateSyntaxProvider(
				predicate: static (node, _) => node is ClassDeclarationSyntax,
				transform: static (ctx, _) => (ClassDeclarationSyntax)ctx.Node);

		context.RegisterSourceOutput(classes, 
				static (ctx, source)=> Execute(ctx, source));
	}

	private static void Execute(SourceProductionContext ctx, ClassDeclarationSyntax source)
	{
		var className = source.Identifier.Text;
		var fileName = $"{className}.g.cs";

		ctx.AddSource(fileName, "// Generated!");

	}
}
