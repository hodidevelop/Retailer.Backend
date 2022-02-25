using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Retailer.Backend.Core.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderState
    {
        None = 0,
        New = 1,
        Billed = 2,
        Paid = 3,
        Closed = 4
    }
}
