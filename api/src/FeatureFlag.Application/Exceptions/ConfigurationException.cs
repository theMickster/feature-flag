using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FeatureFlag.Application.Exceptions;

public class ConfigurationException : Exception
{
    [JsonIgnore]
    public IEnumerable<string> ErrorMessages { get; } = new List<string>();

    #region Constructors

    public ConfigurationException()
    {

    }

    public ConfigurationException(string message) : base(message)
    {

    }

    public ConfigurationException(
        IEnumerable<string> errorMessages,
        string message) : base(message) => ErrorMessages = errorMessages;

    public ConfigurationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ConfigurationException(
        IEnumerable<string> errorMessages,
        string message,
        Exception innerException) : base(message, innerException)
        => ErrorMessages = errorMessages;

    #endregion Constructors
    

    public override string ToString()
    {
        var text = ErrorMessages.Any() ? string.Join(";", ErrorMessages) : "N/A";

        return $"ConfigurationException({Message}: {text})";
    }
}
