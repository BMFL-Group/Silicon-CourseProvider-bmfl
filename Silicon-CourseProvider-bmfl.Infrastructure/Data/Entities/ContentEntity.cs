namespace Silicon_CourseProvider_bmfl.Infrastructure.Data.Entities;

public class ContentEntity
{
    public string? Description { get; set; }
    public string[]? CourseIncludes { get; set; }
    public string[]? WhatYouLearn { get; set; }
    public virtual List<ProgramDetailsEntity>? ProgramDetails { get; set; }
}
