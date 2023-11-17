using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Filtering.Base;

namespace FeatureFlag.Common.Filtering;

/// <summary>
/// Used to support paging in the Feature Flag list feature.
/// </summary>
public sealed class FeatureFlagParameter : QueryStringParamsBase
{
    private const string StoreIdField = "id";
    private const string StoreNameField = "name";
    private string _orderBy = StoreIdField;

    public string OrderBy
    {
        get
        {
            return _orderBy switch
            {
                StoreIdField => SortedResultConstants.FeatureFlagId,
                StoreNameField => SortedResultConstants.Name,
                _ => SortedResultConstants.FeatureFlagId
            };
        }
        set =>
            _orderBy = value?.Trim().ToLower() == StoreIdField ? StoreIdField
                : value?.Trim().ToLower() == StoreNameField ? StoreNameField : StoreIdField;
    }
}
