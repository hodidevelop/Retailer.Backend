using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.Models.DAO.Base;
using Retailer.Backend.Core.Models.Enums;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Retailer.Backend.Core.Models.DAO
{
    public class Order : EntityBase, IOrderRequest
    {
        public OrderState State { get; set; } = OrderState.None;

        [MaxLength(50)]
        public string CustomerName { get; set; }

        [MaxLength(255)]
        public string DeliveryAddress { get; set; }

        [MaxLength(100)]
        public string ProductName { get; set; }

        public double UnitPrice { get; set; }

        public double Quantity { get; set; }

        [ForeignKey(nameof(Invoice))]
        public int? InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
