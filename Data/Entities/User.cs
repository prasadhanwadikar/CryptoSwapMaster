using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ip { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string SecretKey { get; set; }

        [Required]
        public BotStatus BotStatus { get; set; }

        public string BotStatusMsg { get; set; }
    }
}
