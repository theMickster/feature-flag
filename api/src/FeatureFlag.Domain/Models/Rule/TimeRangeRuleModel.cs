﻿namespace FeatureFlag.Domain.Models.Rule;

public sealed class TimeRangeRuleModel
{
    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime EndDate { get; set; } = DateTime.UtcNow;
}
