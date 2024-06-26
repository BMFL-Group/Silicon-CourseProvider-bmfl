﻿namespace Silicon_CourseProvider_bmfl.Infrastructure.Models;

public class Pagination
{
    public int CurrentPage { get; set; }

    public int PageNumber { get; set; }

    public int TotalItems { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }
}