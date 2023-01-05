namespace Trips.Models
{
    public class TripType
    {
        public int TripTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DangerLevel { get; set; }

        public string Preparation { get; set; }
    }
}
