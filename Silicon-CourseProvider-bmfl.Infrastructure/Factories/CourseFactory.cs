using Azure.Core;
using Silicon_CourseProvider_bmfl.Infrastructure.Data.Entities;
using Silicon_CourseProvider_bmfl.Infrastructure.Models;

namespace Silicon_CourseProvider_bmfl.Infrastructure.Factories;

public static class CourseFactory
{
    public static CourseEntity Create(CourseCreateRequest request)
    {
        List<AuthorEntity> authors = [];
        foreach (var author in request.Authors)
        {
            authors.Add(new AuthorEntity { Name = author.Name });
        }
        List<ProgramDetailsEntity> programDetails = [];
        foreach (var programDetail in request.Content.ProgramDetails)
        {
            programDetails.Add(new ProgramDetailsEntity { Id = programDetail.Id, Title = programDetail.Title, Description = [programDetail.Description[0]] });
        }

        return new CourseEntity()
        {
            Title = request.Title,
            Ingress = request.Ingress,
            ImageUri = request.ImageUri,
            AltText = request.AltText,
            BestSeller = request.BestSeller,
            IsDigital = request.IsDigital,
            Categories = request.Categories,
            Currency = request.Currency,
            Price = request.Price,
            DiscountPrice = request.DiscountPrice,
            LengthInHours = request.LengthInHours,
            RatingInPercentage = request.RatingInPercentage,
            NumberOfReviews = request.NumberOfReviews,
            NumberOfLikes = request.NumberOfLikes,

            Authors = authors,
            //Authors = request.Authors?.Select(a => new AuthorEntity
            //{
            //    Name = a.Name,
            //}).ToList(),
            Content = request.Content == null ? null! : new ContentEntity
            {
                Description = request.Content?.Description,
                CourseIncludes = request.Content?.CourseIncludes,
                WhatYouLearn = request.Content?.WhatYouLearn,
                ProgramDetails = programDetails
                //ProgramDetails = request.Content?.ProgramDetails?.Select(pd => new ProgramDetailsEntity
                //{
                //    Id = pd.Id,
                //    Title = pd.Title,
                //    Description = pd.Description,
                //}).ToList()
            }
        };
    }
    public static CourseEntity Create(CourseUpdateRequest request)
    {
        List<AuthorEntity> authors = [];
        foreach (var author in request.Authors)
        {
            authors.Add(new AuthorEntity { Name = author.Name });
        }
        List<ProgramDetailsEntity> programDetails = [];
        foreach (var programDetail in request.Content.ProgramDetails)
        {
            programDetails.Add(new ProgramDetailsEntity { Id = programDetail.Id, Title = programDetail.Title, Description = [programDetail.Description[0]] });
        }


        var course = new CourseEntity()
        {
            Id = request.Id,
            Title = request.Title,
            Ingress = request.Ingress,
            ImageUri = request.ImageUri,
            AltText = request.AltText,
            BestSeller = request.BestSeller,
            IsDigital = request.IsDigital,
            Categories = request.Categories,
            Currency = request.Currency,
            Price = request.Price,
            DiscountPrice = request.DiscountPrice,
            LengthInHours = request.LengthInHours,
            RatingInPercentage = request.RatingInPercentage,
            NumberOfReviews = request.NumberOfReviews,
            NumberOfLikes = request.NumberOfLikes,

            Authors = authors,
            //Authors = request.Authors?.Select(a => new AuthorEntity
            //{
            //    Name = a.Name,
            //}).ToList(),

            Content = request.Content == null ? null! : new ContentEntity
            {
                Description = request.Content?.Description,
                CourseIncludes = request.Content?.CourseIncludes,
                WhatYouLearn = request.Content?.WhatYouLearn,
                ProgramDetails = programDetails,
                //ProgramDetails = request.Content?.ProgramDetails?.Select(pd => new ProgramDetailsEntity
                //{
                //    Id = pd.Id,
                //    Title = pd.Title,
                //    Description = pd.Description,
                //}).ToList()
            }
        };

        return course;
    }
    public static Course Create(CourseEntity entity)
    {

        List<Author> authors = [];
        foreach (var author in entity.Authors)
        {
            authors.Add(new Author { Name = author.Name });
        }
        List<ProgramDetails> programDetails = [];
        foreach (var programDetail in entity.Content.ProgramDetails)
        {
            programDetails.Add(new ProgramDetails { Id = programDetail.Id, Title = programDetail.Title, Description = [programDetail.Description[0]] });
        }

        return new Course()
        {
            Id = entity.Id,
            Title = entity.Title,
            Ingress = entity.Ingress,
            ImageUri = entity.ImageUri,
            AltText = entity.AltText,
            BestSeller = entity.BestSeller,
            IsDigital = entity.IsDigital,
            Categories = entity.Categories,
            Currency = entity.Currency,
            Price = entity.Price,
            DiscountPrice = entity.DiscountPrice,
            LengthInHours = entity.LengthInHours,
            RatingInPercentage = entity.RatingInPercentage,
            NumberOfReviews = entity.NumberOfReviews,
            NumberOfLikes = entity.NumberOfLikes,

            Authors = authors,
            //Authors = entity.Authors?.Select(a => new Author
            //{
            //    Name = a.Name,
            //}).ToList(),

            Content = entity.Content == null ? null! : new Content
            {
                Description = entity.Content?.Description,
                CourseIncludes = entity.Content?.CourseIncludes,
                WhatYouLearn = entity.Content?.WhatYouLearn,
                ProgramDetails = programDetails
                //ProgramDetails = entity.Content?.ProgramDetails?.Select(pd => new ProgramDetails
                //{
                //    Id = pd.Id,
                //    Title = pd.Title,
                //    Description = pd.Description,
                //}).ToList()
            }
        };
    }

    public static IEnumerable<Course> Create(IEnumerable<CourseEntity> entities)
    {
        var courses = new List<Course>();

        foreach (var entity in entities)
        {
            courses.Add(Create(entity));
        }

        return courses;
    }


}
