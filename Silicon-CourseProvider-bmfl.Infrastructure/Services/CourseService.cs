using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Silicon_CourseProvider_bmfl.Infrastructure.Data.Contexts;
using Silicon_CourseProvider_bmfl.Infrastructure.Data.Entities;
using Silicon_CourseProvider_bmfl.Infrastructure.Factories;
using Silicon_CourseProvider_bmfl.Infrastructure.Interfaces;
using Silicon_CourseProvider_bmfl.Infrastructure.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


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

    /// <summary>
    /// A very expensive way to query a database.
    /// </summary>
    /// <param name="filters"></param>
    /// <returns>CourseResult containing courses and pagination</returns>
    public async Task<CourseResult> GetAllCoursesAsync(CourseFilters? filters)
    {
        using var context = _contextFactory.CreateDbContext();

        IEnumerable<CourseEntity> results;

        if (filters != null)
        {
            var query = context.Courses.AsQueryable();

            string searchQuery = "";

            if (filters.SearchQuery != null)
            {
                searchQuery = filters.SearchQuery.ToLower();
            }

            if (!string.IsNullOrEmpty(filters.SearchQuery))
            {
                // Works on its own - not with authors.
                query = query.Where(x => x.Title.ToLower().Contains(searchQuery));
            }

            results = query.AsEnumerable();

            // Should only enter this conditional statement if it cant find any courses by Title.
            if (!string.IsNullOrEmpty(filters.SearchQuery) && results.Count() == 0)
            {
                query = context.Courses.AsQueryable();
                results = query.AsEnumerable();

                // Authors filter. Works when in here.
                results = results.Where(x => x.Authors!.Any(x => x.Name.ToLower().Contains(searchQuery)));
            }

            if (!string.IsNullOrEmpty(filters.Category) && filters.Category != "all")
            {
                // Works
                results = results.Where(x => x.Categories!.Contains(filters.Category));
            }

            results = results.OrderBy(x => x.Title);

            var pagination = new Pagination
            {
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize,
                TotalItems = 0,
                CurrentPage = filters.PageNumber
            };


            pagination.TotalItems = results.Count();
            pagination.TotalPages = (int)Math.Ceiling(pagination.TotalItems / (double)pagination.PageSize);

            var courses = CourseFactory.Create(results.Skip((filters!.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize));

            return new CourseResult
            {
                Courses = courses,
                Pagination = pagination
            };
        }
        
        var nonQueriedCourses = await context.Courses.ToListAsync();
        return new CourseResult()
        {
            Courses = CourseFactory.Create(nonQueriedCourses),
            Pagination = new()
        };
    }


    #region QUERY LOGIC - NOT CURRENTLY USED

    

    public CourseResult SetCourseResult(CourseFilters? filters)
    {
        //var context = SetContext();
        //var query = SetQueryableCourse(context);


        //var searchQuery = ConfigureSearchQuery(filters.SearchQuery);

        //if (!string.IsNullOrEmpty(filters.SearchQuery))
        //{    
        //    query = SearchForTitle(query, searchQuery);
        //    if (query.Count() == 0)
        //    {
        //        query = SetQueryableCourse(context);
        //        results = query.AsEnumerable();



        //        results = SearchForAuthors(results, searchQuery);
        //        return SetCourseResult(results, filters.PageNumber, filters.PageSize);
        //    }
        //    else
        //    {
        //        results = query.AsEnumerable();

        //        if (!string.IsNullOrEmpty(filters.Category) && filters.Category != "all")
        //        {
        //            // Works
        //            results = SetCategory(results, filters.Category);
        //            //results = results.Where(x => x.Categories!.Contains(filters.Category));
        //        }
        //        return SetCourseResult(results, filters.PageNumber, filters.PageSize);
        //    }
        //}

        //results = results.OrderBy(x => x.Title);

        //var pagination = new Pagination
        //{
        //    PageNumber = pageNumber,
        //    PageSize = pageSize,
        //    TotalItems = 0,
        //    CurrentPage = pageNumber
        //};


        //pagination.TotalItems = results.Count();
        //pagination.TotalPages = (int)Math.Ceiling(pagination.TotalItems / (double)pagination.PageSize);

        //var courses = CourseFactory.Create(results.Skip((pageNumber - 1) * pagination.PageSize).Take(pagination.PageSize));

        return new CourseResult
        {
            //Courses = courses,
            //Pagination = pagination
            
        };


    }
    private IEnumerable<CourseEntity> SetCategory(IEnumerable<CourseEntity> results, string category)
    {
        results = results.Where(x => x.Categories!.Contains(category));
        return results;
    }

    private IEnumerable<CourseEntity> SearchForAuthors(IEnumerable<CourseEntity> results, string searchQuery)
    {
        results = results.Where(x => x.Authors!.Any(x => x.Name.ToLower().Contains(searchQuery)));
        return results;
    }

    private IQueryable<CourseEntity> SearchForTitle(IQueryable<CourseEntity> query,string searchQuery)
    {
        // Works on its own - not with authors.
        return query = query.Where(x => x.Title.ToLower().Contains(searchQuery));
    }

    private string ConfigureSearchQuery(string? filterSearchQuery)
    {   
        if (filterSearchQuery != null)
        {
            return filterSearchQuery.ToLower();
        }
        else
        {
            return null!;
        }
    }   

    public IQueryable<CourseEntity> SetQueryableCourse(DataContext context)
    {        
        var query = context.Courses.AsQueryable();
        return query;
    }
    #endregion



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
