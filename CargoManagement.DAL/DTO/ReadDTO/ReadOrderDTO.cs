using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.DAL.DTO.ReadDTO
{
    public class ReadOrderDTO
    {
        public int OrderId { get; set; }
        public int CarrierId { get; set; }
        public int OrderDesi { get; set; }
        public decimal OrderCarrierCost { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
