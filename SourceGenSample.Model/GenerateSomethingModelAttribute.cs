namespace SourceGenSample.Generator;

[AttributeUsage(AttributeTargets.Class)]
public class GenerateSomethingModelAttribute : Attribute
{
    public string ObjName { get; set; } = null!;
}
