using System.ComponentModel.DataAnnotations;

namespace Silicon_CourseProvider_bmfl.Infrastructure.Data.Entities;

public class CourseEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string Ingress { get; set; } = null!;
    public string ImageUri { get; set; } = null!;
    public string AltText { get; set; } = null!;
    public bool BestSeller { get; set; } = false;
    public bool IsDigital { get; set; } = true;
    public string[]? Categories {  get; set; }
    public string Currency { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal DiscountPrice { get; set; }
    public string LengthInHours { get; set; } = null!;
    public int RatingInPercentage { get; set; }
    public int NumberOfReviews { get; set; }
    public int NumberOfLikes { get; set; }
    public virtual List<AuthorEntity>? Authors { get; set; } 
    public virtual ContentEntity? Content { get; set; }
}
