using Microsoft.CodeAnalysis;

namespace SourceGenSample.Generator;

[Generator]
public class SomethingGeneratorFromModel : IIncrementalGenerator
{
    /// <inheritdoc />
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<string> info = context.SyntaxProvider.ForAttributeWithMetadataName("SourceGenSample.Generator.GenerateSomethingModelAttribute", 
                                                                                                     static (_, _) => true,
                                                                                                     static (context, token) =>
                                                                                                     {
                                                                                                         token.ThrowIfCancellationRequested();
                                                                                                         AttributeData attr = context.Attributes[0];
                                                                                                         string? propVal = attr.NamedArguments.First().Value.Value?.ToString();
                                                                                                         return $"//{propVal} and some generated code";
                                                                                                     });
        context.RegisterSourceOutput(info, static (context, source) =>
        {
            context.AddSource("Generated.g.cs", source);
        });
    }
}
