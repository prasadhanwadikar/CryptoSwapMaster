using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int OrderGroup { get; set; }

        [Required]
        public string BaseAsset { get; set; }

        [Required]
        public string QuoteAsset { get; set; }

        [Required]
        public double BaseQty { get; set; }

        [Required]
        public double QuoteQty { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}