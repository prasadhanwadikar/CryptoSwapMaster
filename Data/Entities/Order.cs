using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using CryptoSwapMaster.Data.Enums;

namespace CryptoSwapMaster.Data.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ExchangeId { get; set; }

        [Required]
        public int Pool { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string BaseAsset { get; set; }

        [Required]
        public decimal BaseQty { get; set; }

        public decimal? ExecutedBaseQty { get; set; }

        [Required]
        public string QuoteAsset { get; set; }

        [Required]
        public decimal ExpectedQuoteQty { get; set; }

        public decimal? ReceivedQuoteQty { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public string StatusMsg { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public List<ExchangeOrder> ExchangeOrders { get; set; }

        public Order()
        {
            ExchangeOrders = new List<ExchangeOrder>();
        }
    }
}