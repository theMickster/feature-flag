using FeatureFlag.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FeatureFlag.API.Controllers.Base;

/// <summary>
/// Common class leveraged by all controllers to assist with cross-cutting concerns.
/// </summary>
public abstract class FeatureFlagBaseController : ControllerBase
{
    protected Dictionary<string, string> EventLogInfoParameters = [];
    
    protected Dictionary<string, string> EventLogErrorParameters = [];

    /// <summary>
    /// Common class leveraged by all controllers to assist with cross-cutting concerns.
    /// </summary>
    protected FeatureFlagBaseController()
    {
        EventLogInfoParameters.Add("ServiceName", AppLoggingConstants.ServiceName);
        EventLogInfoParameters.Add("ServiceId", AppLoggingConstants.ServiceId.ToString().ToUpper());
        EventLogInfoParameters.Add(AppLoggingConstants.CurrentDateTime, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));

        EventLogErrorParameters.Add("ServiceName", AppLoggingConstants.ServiceName);
        EventLogErrorParameters.Add("ServiceId", AppLoggingConstants.ServiceId.ToString().ToUpper());
        EventLogErrorParameters.Add(AppLoggingConstants.CurrentDateTime, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
    }
    
}
