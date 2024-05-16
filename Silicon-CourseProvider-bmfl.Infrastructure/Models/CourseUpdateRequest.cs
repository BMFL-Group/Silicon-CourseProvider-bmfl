namespace Silicon_CourseProvider_bmfl.Infrastructure.Models;

public class CourseUpdateRequest
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
    public virtual List<AuthorUpdateRequest>? Authors { get; set; }
    public virtual ContentUpdateRequest? Content { get; set; }
    //public virtual ProgramDetailsUpdateRequest? ProgramDetails { get; set; }
}

public class AuthorUpdateRequest
{
    public string Name { get; set; } = null!;
}

public class ContentUpdateRequest
{
    public string? Description { get; set; }
    public string[]? Courseincludes { get; set; }
    public virtual List<ProgramDetailsUpdateRequest>? ProgramDetails { get; set; }
}

public class ProgramDetailsUpdateRequest
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string[]? Description { get; set; }
}