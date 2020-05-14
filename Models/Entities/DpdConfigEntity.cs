using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Entities
{
    public class DpdConfigEntity
    {
        [Key]
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Channel { get; set; }
        [Required]
        public string EventsSelectType { get; set; }
    }
}
