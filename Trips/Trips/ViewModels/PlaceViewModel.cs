using System.Collections.Generic;
using Trips.Models;

namespace Trips.ViewModels
{
    public class PlaceViewModel
    {
        public string CountrySearch { get; set; }

        public string LocalitySearch { get; set; }

        public OrderParams CountryOrder { get; set; }

        public List<TripPlace> TripPlaces { get; set; }
    }
}
