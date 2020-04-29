using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint4
{
    public class Presentation
    {
        public Guid PresentationId { get; set; }
        public Room Room { get; set; }
        public Show Show { get; set; }
        public DateTime PresentationDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
