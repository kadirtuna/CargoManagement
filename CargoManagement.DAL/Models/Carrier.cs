using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.DAL.Models
{
    public partial class Carrier
    {
        [Key]
        public int CarrierId { get; set; }

        [Required]
        [StringLength(50)]
        public string CarrierName { get; set; } = string.Empty;

        [Required]
        public bool CarrierIsActive { get; set; }

        [Required]
        public int CarrierPlusDesiCost { get; set; }
        
        //Navigation Property
        [Required]
        public virtual CarrierConfiguration CarrierConfiguration { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
