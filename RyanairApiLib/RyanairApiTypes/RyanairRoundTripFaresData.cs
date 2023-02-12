namespace RyanairApiLib.RyanairApiTypes
{
    internal class RyanairRoundTripFaresData
    {
        public const string UriPath = "farfnd/v4/roundTripFares";

        public RyanairRoundTripFair[] Fares { get; set; }
    }
}
