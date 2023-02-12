using System;

namespace RyanairApiLib.Types
{
    public struct Flight
    {
        /// <summary>
        /// The departure time of the flight in the timezone where the departing airport is located.
        /// </summary>
        public DateTime DeperatureTime { get; internal set; }


        /// <summary>
        /// The arrival time of the flight in the timezone where the arriving airport is located.
        /// </summary>
        public DateTime ArrivalTime { get; internal set; }

        /// <summary>
        /// The price of the flight in the configured currency.
        /// </summary>
        public decimal Price { get; internal set; }

        /// <summary>
        /// The departure airport.
        /// </summary>
        public Airport DepartureAirport { get; internal set; }

        /// <summary>
        /// The arrival airport.
        /// </summary>
        public Airport ArrivalAirport { get; internal set; }
    }
}
