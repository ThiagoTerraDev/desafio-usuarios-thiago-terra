using System.Text.Json.Serialization;

namespace api_rest_dotnet.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DepartmentEnum
    {
        RH,
        Financeiro,
        Compras,
        Atendimento,
        Zeladoria,
    }
}
