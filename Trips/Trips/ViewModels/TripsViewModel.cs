using System.Collections.Generic;
using Trips.Models;

namespace Trips.ViewModels
{
    public class TripsViewModel
    {
        public List<Trip> Trips { get; set; }

        public string NameSearch { get; set; }

        public int MaximumCost { get; set; }

        public int MinimumCost { get; set; }

        public OrderParams CostSort { get; set; }
    }
}
