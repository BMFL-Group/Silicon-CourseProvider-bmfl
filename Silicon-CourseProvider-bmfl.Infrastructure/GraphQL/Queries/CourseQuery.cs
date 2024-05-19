using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Silicon_CourseProvider_bmfl.Infrastructure.Interfaces;
using Silicon_CourseProvider_bmfl.Infrastructure.Models;

namespace Silicon_CourseProvider_bmfl.Infrastructure.GraphQL.Queries;

public class CourseQuery(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    [GraphQLName("getCourses")]
    public async Task<CourseResult> GetCoursesAsync(CourseFilters? filterQuery)
    {
        return await _courseService.GetAllCoursesAsync(filterQuery);
    }

    [GraphQLName("getCourseById")]
    public async Task<Course> GetCourseByIdAsync(string id)
    {
        return await _courseService.GetCoursebyIdAsync(id);
    }
}
