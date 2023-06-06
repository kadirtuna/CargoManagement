using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagement.DAL.DTO.ReadDTO
{
    public class ReadCarrierDTO
    {
        public int CarrierId { get; set; }
        public string CarrierName { get; set; } = string.Empty;
        public bool CarrierIsActive { get; set; }
        public int CarrierPlusDesiCost { get; set; }
    }
}
