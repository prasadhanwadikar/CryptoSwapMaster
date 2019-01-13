using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("ExchangeOrders")]
    public class ExchangeOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int Sequence { get; set; }

        public string ExchangeOrderId { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public string Side { get; set; }

        public double BaseQty { get; set; }

        public double? QuoteQty { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public string StatusMsg { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public Order Order { get; set; }
    }
}
