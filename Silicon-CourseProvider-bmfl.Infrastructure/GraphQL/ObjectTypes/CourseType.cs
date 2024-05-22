using Silicon_CourseProvider_bmfl.Infrastructure.Data.Entities;

namespace Silicon_CourseProvider_bmfl.Infrastructure.GraphQL.ObjectTypes;

public class CourseType : ObjectType<CourseEntity>
{
    protected override void Configure(IObjectTypeDescriptor<CourseEntity> descriptor)
    {
        descriptor.Field(c => c.Id).Type<NonNullType<IdType>>();
        descriptor.Field(c => c.Title).Type<StringType>();
        descriptor.Field(c => c.Ingress).Type<StringType>();
        descriptor.Field(c => c.ImageUri).Type<StringType>();
        descriptor.Field(c => c.AltText).Type<StringType>();
        descriptor.Field(c => c.BestSeller).Type<BooleanType>();
        descriptor.Field(c => c.IsDigital).Type<BooleanType>();
        descriptor.Field(c => c.Categories).Type<ListType<StringType>>().UseFiltering();
        descriptor.Field(c => c.Currency).Type<StringType>();
        descriptor.Field(c => c.Price).Type<DecimalType>();
        descriptor.Field(c => c.DiscountPrice).Type<DecimalType>();
        descriptor.Field(c => c.LengthInHours).Type<StringType>();
        descriptor.Field(c => c.RatingInPercentage).Type<IntType>();
        descriptor.Field(c => c.NumberOfReviews).Type<IntType>();
        descriptor.Field(c => c.NumberOfLikes).Type<IntType>();
        descriptor.Field(c => c.Authors).Type<ListType<AuthorType>>().UseFiltering();
        descriptor.Field(c => c.Content).Type<ContentType>();
    }
}
public class AuthorType : ObjectType<AuthorEntity>
{
    protected override void Configure(IObjectTypeDescriptor<AuthorEntity> descriptor)
    {
        descriptor.Field(a => a.Name).Type<StringType>();
    }
}

public class ContentType : ObjectType<ContentEntity>
{
    protected override void Configure(IObjectTypeDescriptor<ContentEntity> descriptor)
    {
        descriptor.Field(c => c.Description).Type<StringType>();
        descriptor.Field(c => c.CourseIncludes).Type<ListType<StringType>>();
        descriptor.Field(c => c.WhatYouLearn).Type<ListType<StringType>>();
        descriptor.Field(c => c.ProgramDetails).Type<ListType<ProgramDetailsType>>();
    }
}

public class ProgramDetailsType : ObjectType<ProgramDetailsEntity>
{
    protected override void Configure(IObjectTypeDescriptor<ProgramDetailsEntity> descriptor)
    {
        descriptor.Field(p => p.Id).Type<IntType>();
        descriptor.Field(p => p.Title).Type<StringType>();
        descriptor.Field(p => p.Description).Type<ListType<StringType>>();
    }
}

