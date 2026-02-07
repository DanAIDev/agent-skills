using System.Collections.Generic;
using System.Linq;
using K2.EVitayCMS.Domain.Common.Consts.Infrastructure;
using K2.EVitayCMS.Domain.Common.DTOs.StaticContent;
using K2.EVitayCMS.Domain.Common.Models.Core;
using K2.EVitayCMS.Domain.Common.Models.StaticContent;
using Riok.Mapperly.Abstractions;

namespace K2.EVitayCMS.Infrastructure.EntityFramework.Mappers;

[Mapper]
public static partial class StaticContentMapper
{
    public static partial IQueryable<StaticContentListDto> ProjectToListDto(this IQueryable<TStaticContent> query);

    public static partial IQueryable<BaseOrderingModel> ProjectToOrdering(this IQueryable<TStaticContent> query);

    [MapProperty(nameof(TStaticContent.StaticContentId), nameof(StaticContentListDto.Id))]
    [MapProperty(new[] { nameof(TStaticContent.ContentType), nameof(TDictionaryItem.Description) }, nameof(StaticContentListDto.StaticContentType))]
    private static partial StaticContentListDto Map(TStaticContent source);

    [MapProperty(nameof(TStaticContent.StaticContentId), nameof(BaseOrderingModel.Id))]
    [MapProperty(nameof(TStaticContent.Title), nameof(BaseOrderingModel.Name))]
    private static partial BaseOrderingModel MapOrdering(TStaticContent source);

    public static StaticContent MapToDomain(TStaticContent source)
    {
        if (source == null)
        {
            return null;
        }

        return new StaticContent
        {
            Id = source.StaticContentId,
            Title = source.Title,
            Content = source.Content,
            IsActive = source.IsActive,
            Order = source.Order,
            StaticContentType = new SimpleDictionaryItem<int>
            {
                Id = source.ContentTypeId,
                Name = source.ContentType?.Description
            },
            TitleTranslations = BuildTranslationDictionary(source.TStaticContentTranslations, t => t.Title),
            ContentTranslations = BuildTranslationDictionary(source.TStaticContentTranslations, t => t.Content)
        };
    }

    public static TStaticContent MapToEntity(StaticContent source)
    {
        if (source == null)
        {
            return null;
        }

        return new TStaticContent
        {
            StaticContentId = source.Id,
            Title = source.Title,
            Content = source.Content,
            IsActive = source.IsActive,
            Order = source.Order,
            ContentTypeId = source.StaticContentType?.Id ?? (int)StaticContentType.Settings
        };
    }

    public static void MapToExistingEntity(StaticContent source, TStaticContent target)
    {
        if (source == null || target == null)
        {
            return;
        }

        target.Title = source.Title;
        target.Content = source.Content;
        target.IsActive = source.IsActive;
        target.Order = source.Order;
        target.ContentTypeId = source.StaticContentType?.Id ?? (int)StaticContentType.Settings;
    }

    private static Dictionary<string, string> BuildTranslationDictionary(
        ICollection<TStaticContentTranslation> translations,
        System.Func<TStaticContentTranslation, string> valueSelector)
    {
        if (translations == null || translations.Count == 0 || translations.Any(item => item.Language == null))
        {
            return new Dictionary<string, string>();
        }

        return translations.ToDictionary(item => item.Language.LanguageCode, valueSelector);
    }
}
