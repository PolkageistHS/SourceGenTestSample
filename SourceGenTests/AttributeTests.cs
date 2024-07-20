using System.Text;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using SourceGenSample.Generator;

namespace SourceGenTests;

public class Tests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public async Task AttributeInSameAssemblyAsGenerator()
    {
        const string source = """
                              using SourceGenSample.Generator;
                              namespace TestNamespace;

                              [GenerateSomething(ObjName = "MyObj")]
                              public partial class GenerateThis;
                              """;
        const string result = "//MyObj and some generated code";
        CSharpSourceGeneratorTest<SomethingGenerator, DefaultVerifier> context = new()
        {
            TestState =
            {
                Sources = { source },
                GeneratedSources = { (@"SourceGenSample.Generator\SourceGenSample.Generator.SomethingGenerator\Generated.g.cs", SourceText.From(result, Encoding.UTF8)) },
                AdditionalReferences = { typeof(SomethingGenerator).Assembly }
            }
        };
        await context.RunAsync();
        Assert.Pass();
    }

    [Test]
    public async Task AttributeInDifferentAssembly()
    {
        const string source = """
                              using SourceGenSample.Generator;
                              namespace TestNamespace;

                              [GenerateSomethingModel(ObjName = "MyObj")]
                              public partial class GenerateThis;
                              """;
        const string result = "//MyObj and some generated code";
        CSharpSourceGeneratorTest<SomethingGeneratorFromModel, DefaultVerifier> context = new()
        {
            TestState =
            {
                Sources = { source },
                GeneratedSources = { (@"SourceGenSample.Generator\SourceGenSample.Generator.SomethingGeneratorFromModel\Generated.g.cs", SourceText.From(result, Encoding.UTF8)) },
                AdditionalReferences = { typeof(SomethingGeneratorFromModel).Assembly, typeof(GenerateSomethingModelAttribute).Assembly }
            }
        };
        await context.RunAsync();
        Assert.Pass();
    }
}
