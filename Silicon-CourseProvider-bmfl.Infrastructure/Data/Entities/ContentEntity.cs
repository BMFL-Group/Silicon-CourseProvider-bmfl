namespace Silicon_CourseProvider_bmfl.Infrastructure.Data.Entities;

public class ContentEntity
{
    public string? Description { get; set; }
    public string[]? Courseincludes { get; set; }
    public virtual List<ProgramDetailsEntity>? ProgramDetails { get; set; }
}
