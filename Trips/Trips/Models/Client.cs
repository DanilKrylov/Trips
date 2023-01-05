
using System.Collections.Generic;

namespace Trips.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronomic { get; set; }

        public string PassportCode { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public List<TripClient> TripClients { get; set; } = new();
    }
}
