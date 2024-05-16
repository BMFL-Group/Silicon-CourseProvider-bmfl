using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Silicon_CourseProvider_bmfl.Infrastructure.Interfaces;
using Silicon_CourseProvider_bmfl.Infrastructure.Models;

namespace Silicon_CourseProvider_bmfl.Infrastructure.GraphQL.Queries;

public class CourseQuery(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    [GraphQLName("getCourses")]
    public async Task<IActionResult> GetCoursesAsync()
    {
        try
        {
            var result = await _courseService.GetAllCoursesAsync();
            if(result != null)
            {
                return new OkObjectResult(result);
            }

            return new NotFoundResult();
        }
        catch
        {
            return new StatusCodeResult(500);
        }
    }

    [GraphQLName("getCourseById")]
    public async Task<IActionResult> GetCourseByIdAsync(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id))
            {
                var result = await _courseService.GetCoursebyIdAsync(id);
                if (result != null)
                {
                    return new OkObjectResult(result);
                }

                return new NotFoundResult();
            }

            return new BadRequestObjectResult($"parameter string Id: {id}, was null or empty");
        }
        catch
        {
            return new StatusCodeResult(500);
        }
    }
}
