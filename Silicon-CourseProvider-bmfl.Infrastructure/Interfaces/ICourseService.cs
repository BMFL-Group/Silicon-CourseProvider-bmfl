﻿using Silicon_CourseProvider_bmfl.Infrastructure.Models;

namespace Silicon_CourseProvider_bmfl.Infrastructure.Interfaces;

public interface ICourseService
{
    Task<Course> CreateCourseAsync(CourseCreateRequest request);
    Task<bool> DeleteCourseAsync(string id);
    Task<CourseResult> GetAllCoursesAsync(CourseFilters? filterQuery);
    Task<Course> GetCoursebyIdAsync(string id);
    Task<Course> UpdateCourseAsync(CourseUpdateRequest request);
}