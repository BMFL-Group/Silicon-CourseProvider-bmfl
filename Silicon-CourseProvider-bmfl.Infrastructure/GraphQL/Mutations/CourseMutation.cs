using Microsoft.AspNetCore.Mvc;
using Silicon_CourseProvider_bmfl.Infrastructure.Interfaces;
using Silicon_CourseProvider_bmfl.Infrastructure.Models;

namespace Silicon_CourseProvider_bmfl.Infrastructure.GraphQL.Mutations;

public class CourseMutation(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    [GraphQLName("createCourse")]
    public async Task<Course> CreateCourseAsync(CourseCreateRequest request)
    {
            var result = await _courseService.CreateCourseAsync(request);
        return result;
    }

    [GraphQLName("updateCourse")]
    public async Task<Course> UpdateCourseAsync(CourseUpdateRequest request)
    {
        var result = await _courseService.UpdateCourseAsync(request);
        return result;
    }

    [GraphQLName("deleteCourse")]
    public async Task<bool> DeleteCourseAsync(string courseId)
    {
        var result = await _courseService.DeleteCourseAsync(courseId);
        return result;
    }

}
