using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint4
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public int TotalSeats { get; set; }
        public ICollection<Presentation> Presentations { get; set; }
    }
}
