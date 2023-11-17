﻿using FeatureFlag.Common.Constants;
using FeatureFlag.Domain.Entities.Base;

namespace FeatureFlag.Domain.Entities;

public sealed class RuleTypeEntity : BaseMetadataEntity
{
    public string TypeName = PartitionKeyConstants.RuleType;

    public override Guid TypeId { get; set; } = PartitionKeyConstants.RuleTypeGuid;
}
