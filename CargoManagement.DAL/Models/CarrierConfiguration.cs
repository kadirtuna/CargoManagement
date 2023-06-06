using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.DAL.Models
{
    public partial class CarrierConfiguration
    {
        [Key, ForeignKey(nameof(Carrier))]
        public int CarrierId { get; set; }

        [Required]
        public int CarrierMaxDesi { get; set; }

        [Required]
        public int CarrierMinDesi { get; set; }

        [Required]
        public decimal CarrierCost { get; set; }

        //The Navigation Property
        [Required]
        public virtual Carrier Carrier { get; set; }
    }
}
