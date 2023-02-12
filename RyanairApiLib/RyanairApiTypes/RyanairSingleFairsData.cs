namespace RyanairApiLib.RyanairApiTypes
{
    internal class RyanairSingleFairsData
    {
        public const string UriPath = "farfnd/v4/oneWayFares";

        public RyanairSingleFair[] Fares { get; set; }
    }
}
