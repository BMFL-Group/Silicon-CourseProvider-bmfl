namespace Silicon_CourseProvider_bmfl.Infrastructure.Models;

public class CourseResult
{
    public IEnumerable<Course> Courses { get; set; } = [];
    public Pagination Pagination { get; set; } = new();
    public CategoryModel Category { get; set; } = new();
}

