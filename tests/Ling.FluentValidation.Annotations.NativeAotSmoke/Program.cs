using Ling.FluentValidation.Annotations;

var dto = new SampleDto
{
    Name = "AOT",
    Mode = nameof(SampleMode.Fast),
};

if (dto.Name != "AOT" || dto.Mode != nameof(SampleMode.Fast))
{
    return 1;
}

Console.WriteLine("Ling.FluentValidation.Annotations Native AOT smoke passed.");
return 0;

public enum SampleMode
{
    Standard = 0,
    Fast = 1,
}

[GenerateValidator]
public sealed class SampleDto
{
    [NotEmpty]
    [Length(2, 32)]
    public string Name { get; set; } = string.Empty;

    [EnumName<SampleMode>]
    public string Mode { get; set; } = string.Empty;
}
