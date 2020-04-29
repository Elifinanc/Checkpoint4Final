using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint4
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Person Person { get; set; }
        public Presentation Presentation { get; set; }
        public int ReservedSeats { get; set; }
    }
}
