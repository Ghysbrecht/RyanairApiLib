namespace RyanairApiLib.Types
{
    public struct Airport
    {
        /// <summary>
        /// The three letter IATA code of the airport.
        /// </summary>
        public string IataCode { get; internal set; }

        /// <summary>
        /// The name of the airport.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The full name of the country where the airport is located in.
        /// </summary>
        public string Country { get; internal set; }
    }
}
