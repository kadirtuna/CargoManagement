using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.DAL.Models
{
    public partial class Order
    {
        [Key]
        public int OrderId { get; set; }
        
        [Required, ForeignKey(nameof(Carrier))]
        public int CarrierId { get; set; }
        public virtual Carrier Carrier { get; set; }    

        [Required]
        public int OrderDesi { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal OrderCarrierCost { get; set; }   
    }
}
