using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Silicon_CourseProvider_bmfl.Infrastructure.Data.Contexts;
using Silicon_CourseProvider_bmfl.Infrastructure.Factories;
using Silicon_CourseProvider_bmfl.Infrastructure.Interfaces;
using Silicon_CourseProvider_bmfl.Infrastructure.Models;

namespace Silicon_CourseProvider_bmfl.Infrastructure.Services;

public class CourseService(IDbContextFactory<DataContext> context, ILogger<CourseService> logger) : ICourseService
{
    private readonly IDbContextFactory<DataContext> _contextFactory = context;
    private readonly ILogger<CourseService> _logger = logger;

    public async Task<Course> CreateCourseAsync(CourseCreateRequest request)
    {
        if (request != null)
        {
            using var context = _contextFactory.CreateDbContext();
            var entity = CourseFactory.Create(request);
            try
            {
                var existingEntity = await context.Courses.FirstOrDefaultAsync(x => x.Title == request.Title);
                if ( existingEntity == null)
                {
                    context.Courses.Add(entity);
                    await context.SaveChangesAsync();
                    return CourseFactory.Create(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: CourseService.CreateCourseAsync() :: {ex.Message}");
            }
        }
        return null!;
    }


    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        using var context = _contextFactory.CreateDbContext();

        try
        {
            var courses = await context.Courses.ToListAsync();
            if (courses != null && courses.Count() > 0)
            {
                //return courses.Select(CourseFactory.Create);
                return CourseFactory.Create(courses);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR: CourseService.GetAllCoursesAsync() :: {ex.Message}");
        }
        return null!;    
    }

    public async Task<Course> GetCoursebyIdAsync(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            using var context = _contextFactory.CreateDbContext();

            try
            {
                var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
                if (course != null)
                {
                    return CourseFactory.Create(course);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: CourseService.GetCoursebyIdAsync() :: {ex.Message}");
            }
        }
        return null!;
    }

    public async Task<Course> UpdateCourseAsync(CourseUpdateRequest request)
    {
        if (request != null)
        {
            using var context = _contextFactory.CreateDbContext();

            var updatedEntity = CourseFactory.Create(request);
            var existingEntity = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingEntity != null)
            {
                updatedEntity.Id = existingEntity.Id;
                context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
                try
                {
                    await context.SaveChangesAsync();
                    return CourseFactory.Create(updatedEntity);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR: CourseService.UpdateCourseAsync() :: {ex.Message}");
                }
            }
        }
        return null!;
    }
    public async Task<bool> DeleteCourseAsync(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            using var context = _contextFactory.CreateDbContext();
            var entity = await context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if(entity != null)
            {
                context.Courses.Remove(entity);
                try
                {
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR: CourseService.DeleteCourseAsync() :: {ex.Message}");
                }
            }
        }
        return false!;
    }
}

//public interface ICourseService
//{
//    Task<Course> CreateCourseAsync(CourseCreateRequest request);
//    Task<IEnumerable<Course>> GetAllCoursesAsync();
//    Task<Course> GetCoursebyIdAsync(string id);
//    Task<Course> UpdateCourseAsync(CourseUpdateRequest request);
//    Task<bool> DeleteCourseAsync(string id);
//}