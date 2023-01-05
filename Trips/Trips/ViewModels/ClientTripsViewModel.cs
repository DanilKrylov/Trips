using System;
using System.Collections.Generic;
using Trips.Models;

namespace Trips.ViewModels
{
    public class ClientTripsViewModel
    {
        public List<TripClient>  TripClients { get; set; }

        public DateTime MinDateTime { get; set; }

        public DateTime MaxDateTime { get; set; }
    }
}
