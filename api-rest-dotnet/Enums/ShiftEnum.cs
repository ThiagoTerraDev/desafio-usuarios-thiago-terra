using System.Text.Json.Serialization;

namespace api_rest_dotnet.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShiftEnum
    {
        Manha,
        Tarde,
        Noite
    }
}
