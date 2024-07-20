namespace SourceGenSample.Generator;

[AttributeUsage(AttributeTargets.Class)]
public class GenerateSomethingAttribute : Attribute
{
    public string ObjName { get; set; } = null!;
}
