using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoSwapMaster.Data.Entities
{
    [Table("QuoteAssets")]
    public class QuoteAsset
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ExchangeId { get; set; }

        [Required]
        public string Asset { get; set; }
    }
}