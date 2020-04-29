using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint4
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Person(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public Person()
        { }
    }
}
