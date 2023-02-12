# RyanairApiLib

This simple .NET standard 2.1 class library can be used to retrieve one way fares and round trips from the Ryanair API. This API does not require authentiation but has a rate limit.

Inspired by: https://github.com/cohaolain/ryanair-py

## Examples

```csharp
var api = new RyanairApi("EUR");

// Get the cheapest flights departing from Brussels Airport, Belgium between the 12th and the 14th of February 2023.
var flights = api.GetOneWayFlights("BRU", new DateTime(2023, 2, 12), new DateTime(2023, 2, 14));
```

```csharp
// Get the cheapest return trips departing from Brussels Airport, Belgium between the 12th and the 14th of February 2023 and returning on the 20th.
var trips = api.GetReturnTrips("BRU", new DateTime(2023, 2, 12), new DateTime(2023, 2, 14), new DateTime(2023, 2, 20), new DateTime(2023, 2, 20));
```