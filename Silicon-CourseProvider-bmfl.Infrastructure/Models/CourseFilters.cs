namespace Silicon_CourseProvider_bmfl.Infrastructure.Models;

public class CourseFilters
{
    public string? Category { get; set; }
    public string? SearchQuery { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}