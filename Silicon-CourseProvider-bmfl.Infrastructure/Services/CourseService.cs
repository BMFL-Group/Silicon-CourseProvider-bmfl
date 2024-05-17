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
        using var context = _contextFactory.CreateDbContext();

        var entity = CourseFactory.Create(request);
        context.Courses.Add(entity);
        await context.SaveChangesAsync();
        return CourseFactory.Create(entity);
    }


    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        using var context = _contextFactory.CreateDbContext();

        var courses = await context.Courses.ToListAsync();
        return CourseFactory.Create(courses);

    }

    public async Task<Course> GetCoursebyIdAsync(string id)
    {
        using var context = _contextFactory.CreateDbContext();
        var course = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        return CourseFactory.Create(course!);
    }

    public async Task<Course> UpdateCourseAsync(CourseUpdateRequest request)
    {
        using var context = _contextFactory.CreateDbContext();
        var existingEntity = await context.Courses.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingEntity != null)
        {
            var updatedEntity = CourseFactory.Create(request);
            updatedEntity.Id = existingEntity.Id;
            context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            context.Entry(existingEntity).State = EntityState.Modified;
            var result = await context.SaveChangesAsync();
            if(result == 1)
            {
                string bla = "";
                return CourseFactory.Create(updatedEntity);
            }
        }
        
        return null!;                
    }
    public async Task<bool> DeleteCourseAsync(string id)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        if (entity != null)
        {
            context.Courses.Remove(entity);
            await context.SaveChangesAsync();
        }
            
        return true;        
    }
}
