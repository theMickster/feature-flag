﻿using Newtonsoft.Json;

namespace FeatureFlag.Domain.Entities.Base;

public abstract class BaseEntity
{
    [JsonProperty("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
}