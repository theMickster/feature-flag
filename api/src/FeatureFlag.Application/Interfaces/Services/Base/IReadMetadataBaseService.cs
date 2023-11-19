using FeatureFlag.Domain.Models.Base;

namespace FeatureFlag.Application.Interfaces.Services.Base;

public interface IReadMetadataBaseService<out T> where T : MetadataBaseModel
{
    /// <summary>
    /// Retrieve a list of metadata models.
    /// </summary>
    /// <remarks>The cache will be checked prior to retrieving metadata from Cosmos Db.</remarks>
    /// <returns>List of type <typeparam name="T"></typeparam></returns>
    IReadOnlyList<T> GetList();

    /// <summary>
    /// Retrieve a single metadata model by it's unique indecenter.
    /// </summary>
    /// <remarks>The cache will be checked prior to retrieving metadata from Cosmos Db.</remarks>
    /// <returns>A single <typeparam name="T"></typeparam></returns>
    T? GetModelById(Guid id);

    /// <summary>
    /// Retrieve a single metadata model by it's name.
    /// </summary>
    /// <remarks>The cache will be checked prior to retrieving metadata from Cosmos Db.</remarks>
    /// <returns>A single <typeparam name="T"></typeparam></returns>
    T? GetModelByName(string name);
}

