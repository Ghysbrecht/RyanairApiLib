namespace RyanairApiLib.Types
{
    public struct Trip
    {
        /// <summary>
        /// Total price of the round trip.
        /// </summary>
        public decimal TotalPrice => Outbound.Price + Inbound.Price;

        /// <summary>
        /// The outbound flight at the start of the trip.
        /// </summary>
        public Flight Outbound { get; internal set; }

        /// <summary>
        /// The inbound flight at the end of the trip.
        /// </summary>
        public Flight Inbound { get; internal set; }
    }
}
