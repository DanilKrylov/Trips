using System.Collections.Generic;
using System.ComponentModel;

namespace Trips.Models
{
    public class Trip
    {
        public int TripId { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public string Description { get; set; }

        public int DaysCount { get; set; }

        public string LivingType { get; set; }

        public bool FoodAvailable { get; set; }

        public TripType TripType { get; set; }
        public int TripTypeId { get; set; }


        public WayOfTrip WayOfTrip { get; set; }
        public int WayOfTripId { get; set; }


        public TripPlace TripPlace { get; set; }
        public int TripPlaceId { get; set; }

        public List<TripClient> TripClients { get; set; } = new();
    }
}
