﻿using FeatureFlag.Common.Constants;
using FeatureFlag.Common.Filtering.Base;

namespace FeatureFlag.Common.Filtering;

/// <summary>
/// Used to support paging in the Feature Flag list feature.
/// </summary>
public sealed class FeatureFlagParameter : QueryStringParamsBase
{
    private const string IdField = "id";
    private const string NameField = "name";
    private string _orderBy = IdField;

    public string OrderBy
    {
        get
        {
            return _orderBy switch
            {
                IdField => SortedResultConstants.FeatureFlagId,
                NameField => SortedResultConstants.Name,
                _ => SortedResultConstants.FeatureFlagId
            };
        }
        set =>
            _orderBy = value?.Trim().ToLower() == IdField ? IdField
                : value?.Trim().ToLower() == NameField ? NameField : IdField;
    }
}
