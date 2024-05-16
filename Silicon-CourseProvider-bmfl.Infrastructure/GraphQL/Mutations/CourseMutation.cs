using Microsoft.AspNetCore.Mvc;
using Silicon_CourseProvider_bmfl.Infrastructure.Interfaces;
using Silicon_CourseProvider_bmfl.Infrastructure.Models;

namespace Silicon_CourseProvider_bmfl.Infrastructure.GraphQL.Mutations;

public class CourseMutation(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    [GraphQLName("getCourses")]
    public async Task<IActionResult> CreateCourseAsync(CourseCreateRequest request)
    {
        try
        {
            if (request != null)
            {
                var result = await _courseService.CreateCourseAsync(request);
                if (result != null)
                {
                    return new OkObjectResult(result);
                }

                return new BadRequestObjectResult($"Course already exists or could not be created, please try again.");
            }

            return new BadRequestObjectResult($"parameter CourseCreateRequest: {request}, was null.");
        }
        catch
        {
            return new StatusCodeResult(500);
        }
    }

    [GraphQLName("UpdateCourse")]
    public async Task<IActionResult> UpdateCourseAsync(CourseUpdateRequest request)
    {
        try
        {
            if (request != null)
            {
                var result = await _courseService.UpdateCourseAsync(request);
                if (result != null)
                {
                    return new OkObjectResult(result);
                }

                return new BadRequestObjectResult($"Course does not exists or could not be updated, please try again.");
            }

            return new BadRequestObjectResult($"parameter CourseUpdateRequest: {request}, was null.");
        }
        catch
        {
            return new StatusCodeResult(500);
        }
    }

    [GraphQLName("DeleteCourse")]
    public async Task<IActionResult> DeleteCourseAsync(string  id)
    {
        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                var result = await _courseService.DeleteCourseAsync(id);
                if (result)
                {
                    return new OkResult();
                }

                return new BadRequestObjectResult($"Course does not exists or could not be updated, please try again.");
            }

            return new BadRequestObjectResult($"parameter string id: {id}, was null or empty.");
        }
        catch
        {
            return new StatusCodeResult(500);
        }
    }

}
