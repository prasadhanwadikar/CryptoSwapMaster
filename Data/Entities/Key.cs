using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoSwapMaster.Data.Entities
{
    [Table("Keys")]
    public class Key
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ExchangeId { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string SecretKey { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public User User { get; set; }
    }
}
