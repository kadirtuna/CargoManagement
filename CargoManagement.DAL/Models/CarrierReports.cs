using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.DAL.Models
{
    public class CarrierReports
    {
        [Key]
        public int CarrierReportId { get; set; }
        [Required]
        public int CarrierId { get; set; }
        [Required]
        public decimal CarrierCost { get; set; }
        [Required]
        public DateTime CarrierReportDate { get; set; }
    }
}
