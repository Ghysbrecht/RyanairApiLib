using RyanairApiLib.RyanairApiTypes;
using RyanairApiLib.Types;

namespace RyanairApiLib.Mappers
{
    internal class RyanairToLibTypesMapper
    {
        public static Trip Map(RyanairRoundTripFair input)
        {
            return new Trip()
            {
                Inbound = Map(input.Inbound),
                Outbound = Map(input.Outbound),
            };
        }

        public static Flight Map(RyanairFlight input)
        {
            return new Flight()
            {
                DeperatureTime = input.DepartureDate,
                ArrivalTime = input.ArrivalDate,
                Price = input.Price.Value,
                DepartureAirport = Map(input.DepartureAirport),
                ArrivalAirport = Map(input.ArrivalAirport)
            };
        }

        public static Airport Map(RyanairAirport input)
        {
            return new Airport()
            {
                IataCode = input.IataCode,
                Name = input.Name,
                Country = input.CountryName
            };
        }
    }
}
