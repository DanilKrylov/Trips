using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;
using Trips.Models;

namespace Trips
{
    public class DatabaseStore : DbContext
    {
        public DatabaseStore()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Trips;Trusted_Connection=True;");
        }


        public DbSet<Trips.Models.Client> Client { get; set; }


        public DbSet<Trips.Models.Trip> Trip { get; set; }


        public DbSet<Trips.Models.TripClient> TripClient { get; set; }


        public DbSet<Trips.Models.TripPlace> TripPlace { get; set; }


        public DbSet<Trips.Models.TripType> TripType { get; set; }


        public DbSet<Trips.Models.WayOfTrip> WayOfTrip { get; set; }
    }
}
