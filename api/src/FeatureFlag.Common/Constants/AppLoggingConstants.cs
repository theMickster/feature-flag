namespace FeatureFlag.Common.Constants;

public static class AppLoggingConstants
{
    public static readonly string ServiceName = "FeatureFlagAPI";

    public static readonly Guid ServiceId = new("bd5b129f-9ac0-4b7d-b23b-ec8fe8945b14");

    public static readonly string Operation = "Operation";

    public static readonly string OperationFeatureFlagStatusRead = "Read Feature Flag Status";

    public static readonly string CurrentDateTime = "CurrentDateTime";

    public static readonly string Status = "Status";
    
    public static readonly string Errors = "Errors"; 

    public const string StatusOk = "Status200OK";

    public const string StatusBadRequest = "Status400BadRequest";

    public const string StatusNotFound = "Status404NotFound";

    public const string HttpGetRequestErrorCode = "Http-Get-Error-001";

    public const string HttpPostRequestErrorCode = "Http-Post-Error-001";

    public const string HttpPutRequestErrorCode = "Http-Put-Error-001";
    
    
}
