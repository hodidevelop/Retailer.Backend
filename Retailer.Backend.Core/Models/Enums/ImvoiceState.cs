using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Retailer.Backend.Core.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InvoiceState
    {
        None = 0,
        New = 1,
        Paid = 2,
        Closed = 3
    }
}
