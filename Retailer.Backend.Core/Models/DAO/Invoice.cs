using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Retailer.Backend.Core.Models.DAO.Base;
using Retailer.Backend.Core.Models.Enums;

using System.ComponentModel.DataAnnotations;

namespace Retailer.Backend.Core.Models.DAO
{
    public class Invoice : EntityBase
    {
        public InvoiceState State { get; set; } = InvoiceState.None;

        [MaxLength(255)]
        public string BillingAddress { get; set; }

        public double Amount { get; set; }

        public double AmountPaid { get; set; }
    }
}
