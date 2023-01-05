using System;

namespace Trips.Models
{
    public class TripClient
    {
        public int TripClientId { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public DateTime StartDate { get; set; }
    }
}
