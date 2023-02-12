using System;

namespace RyanairApiLib.RyanairApiTypes
{
    internal class RyanairFlight
    {
        public RyanairAirport DepartureAirport { get; set; }
        public RyanairAirport ArrivalAirport { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public RyanairPrice Price { get; set; }
        public string FlightNumber { get; set; }
        public long PriceUpdated { get; set; }
    }
}
