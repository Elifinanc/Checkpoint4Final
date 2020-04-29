using Microsoft.EntityFrameworkCore;

namespace Checkpoint4
{
    public class ShowContext: DbContext
    {
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Presentation> Presentations { get; set; }
        public virtual DbSet<PresentationInfo> PresentationInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LOCALHOST\SQLEXPRESS;Database=Checkpoint4;Integrated Security=True;MultipleActiveResultSets=true");
        }
    }
}
