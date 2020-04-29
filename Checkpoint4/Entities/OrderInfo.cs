using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint4
{
    public class OrderInfo
    {
        public Guid OrderInfoId { get; set; }
        public string PresentationId { get; set; }
        public string ShowName { get; set; }
        public DateTime PresentationDate { get; set; }
        public int ReservedSeats { get; set; }

        public OrderInfo() { }
    }
}
