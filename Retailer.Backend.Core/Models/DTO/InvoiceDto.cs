using Retailer.Backend.Core.Models.Enums;

using System.ComponentModel.DataAnnotations;

namespace Retailer.Backend.Core.Models.DTO
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public InvoiceState State { get; set; } = InvoiceState.None;

        [MaxLength(255)]
        public string BillingAddress { get; set; }

        public double Amount { get; set; }

        public double AmountPaid { get; set; }
    }
}
