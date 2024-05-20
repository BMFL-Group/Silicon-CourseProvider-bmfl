using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Silicon_CourseProvider_bmfl.Infrastructure.Data.Contexts;
using Silicon_CourseProvider_bmfl.Infrastructure.Factories;
using Silicon_CourseProvider_bmfl.Infrastructure.Interfaces;
using Silicon_CourseProvider_bmfl.Infrastructure.Models;
using System.Diagnostics;


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


    public async Task<CourseResult> GetAllCoursesAsync(CourseFilters? filters)
    {
        using var context = _contextFactory.CreateDbContext();

        if (filters != null)
        {
            var query = context.Courses.AsQueryable();
            string searchQuery = "";

            if(filters.SearchQuery != null)
            {
                searchQuery = filters.SearchQuery.ToLower();
            }

            if (!string.IsNullOrEmpty(filters.Category) && filters.Category != "all")
            {
                // not working
                //query = query.Where(x => x.Categories.Any(x => x.Equals(filters.Category)));
            }

            if (!string.IsNullOrEmpty(filters.SearchQuery))
            {
                //title now works to search for.
                query = query.Where(x => x.Title.ToLower().Contains(searchQuery));


                //authors do not work to search for yet.
            }

            query = query.OrderBy(x => x.Title);

            var pagination = new Pagination
            {
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize,
                TotalItems = 0,
                CurrentPage = filters.PageNumber
            };

            if (query != null && query.Count() > 0)
            {
                pagination.TotalItems = query.Count();
            }

            pagination.TotalPages = (int)Math.Ceiling(pagination.TotalItems / (double)pagination.PageSize);
            var courses = CourseFactory.Create(await query.Skip((filters!.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync());

            return new CourseResult
            {
                Courses = courses,
                Pagination = pagination
            };
        }
        else
        {
            var nonQueriedCourses = await context.Courses.ToListAsync();
            return new CourseResult()
            {
                Courses = CourseFactory.Create(nonQueriedCourses),
                Pagination = new()
            };
        }

    }

    public async Task<Course> GetCoursebyIdAsync(string id)
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
            Debug.WriteLine(ex.Message);
        }
        return null!;
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
