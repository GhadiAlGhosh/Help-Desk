using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string DeviceType { get; set; }
        public string SerialNumber { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public Customer Customer { get; set; }
        public bool IsWarranty { get; set; }
        public bool IsCritical { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
