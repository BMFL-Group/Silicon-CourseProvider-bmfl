namespace Silicon_CourseProvider_bmfl.Infrastructure.Models;

public class Course
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public string ImageUri { get; set; } = null!;
    public string AltText { get; set; } = null!;
    public bool BestSeller { get; set; } = false;
    public bool IsDigital { get; set; } = true;
    public string[]? Categories { get; set; }
    public string Currency { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public string LengthInHours { get; set; } = null!;
    public int RatingInPercentage { get; set; }
    public int NumberOfReviews { get; set; }
    public int NumberOfLikes { get; set; }
    public virtual List<Author>? Authors { get; set; }
    public virtual Content? Content { get; set; }
    public virtual ProgramDetails? ProgramDetails { get; set; }
}

public class Author
{
    public string Name { get; set; } = null!;
}

public class Content
{
    public string? Description { get; set; }
    public string[]? CourseIncludes { get; set; }
    public string[]? WhatYouLearn { get; set; }

    public virtual List<ProgramDetails>? ProgramDetails { get; set; }
}

public class ProgramDetails
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string[]? Description { get; set; }
}