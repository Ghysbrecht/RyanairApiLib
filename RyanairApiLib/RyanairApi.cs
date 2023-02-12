using RyanairApiLib.Exceptions;
using RyanairApiLib.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using System.Text.Json;
using RyanairApiLib.RyanairApiTypes;
using RyanairApiLib.Mappers;

namespace RyanairApiLib
{
    public class RyanairApi
    {
        private const string SchemeName = "https";
        private const string Host = "services-api.ryanair.com";
        private const string DateFormat = "yyyy-MM-dd";

        private readonly string _currency;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;
        
        public RyanairApi(string currency = "EUR")
        {
            _currency = currency;
            _client = new HttpClient();
            _jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Retrieves a list of the cheapest flights from the given airport between the given from and to time.
        /// You can optionally provide a destination country code to limit the results. (e.g. "BE")
        /// Note that not all flights are included, but only the cheapest. There may be equally priced or slightly more expensive flights with more interesting timing.
        /// </summary>
        /// <param name="departureAirportIataCode">The IATA code of the departure airport. (e.g. "BRU" for Brussels Airport in Belgium)</param>
        /// <param name="outboundFrom">Start of the outbound flight date range.</param>
        /// <param name="outboundTo">End of the outbound flight date range. Can be equal to the <paramref name="outboundFrom"/> date.</param>
        /// <param name="destinationCountryCode">Optional country code of the destination airport to limit the resutls.</param>
        /// <returns>Collection of the cheapest flights between the given date range.</returns>
        /// <exception cref="ApiRequestFailedException">Thrown when the request to the Ryanair API has failed.</exception>
        public async Task<IReadOnlyCollection<Flight>> GetOneWayFlights(string departureAirportIataCode, DateTime outboundFrom, DateTime outboundTo, string destinationCountryCode = null)
        {
            var query = new Dictionary<string, string>
            {
                ["departureAirportIataCode"] = departureAirportIataCode,
                ["outboundDepartureDateFrom"] = outboundFrom.ToString(DateFormat),
                ["outboundDepartureDateTo"] = outboundTo.ToString(DateFormat),
                ["currency"] = _currency
            };

            if (!string.IsNullOrWhiteSpace(destinationCountryCode))
            {
                query["arrivalCountryCode"] = destinationCountryCode;
            }

            var response = await GetResponse<RyanairSingleFairsData>(RyanairSingleFairsData.UriPath, query);

            return response.Fares.Select(one => RyanairToLibTypesMapper.Map(one.Outbound)).ToList();
        }

        /// <summary>
        /// Retrieves a list of the cheapest round trip flights from the given airport between the given from and to times.
        /// </summary>
        /// <param name="departureAirportIataCode"></param>
        /// <param name="outboundFrom">Start of the outbound flight date range.</param>
        /// <param name="outboundTo">End of the outbound flight date range. Can be equal to the <paramref name="outboundFrom"/> date.</param>
        /// <param name="inboundFrom">Start of the inbound flight date range.</param>
        /// <param name="inboundTo">End of the inbound flight date range. Can be equal to the <paramref name="inboundFrom"/> date.</param>
        /// <returns>Collection of the cheapest round trup flights considering the given date ranges.</returns>
        /// <exception cref="ApiRequestFailedException">Thrown when the request to the Ryanair API has failed.</exception>
        public async Task<IReadOnlyCollection<Trip>> GetReturnTrips(string departureAirportIataCode, DateTime outboundFrom, DateTime outboundTo, DateTime inboundFrom, DateTime inboundTo)
        {
            var query = new Dictionary<string, string>
            {
                ["departureAirportIataCode"] = departureAirportIataCode,
                ["outboundDepartureDateFrom"] = outboundFrom.ToString(DateFormat),
                ["outboundDepartureDateTo"] = outboundTo.ToString(DateFormat),
                ["inboundDepartureDateFrom"] = inboundFrom.ToString(DateFormat),
                ["inboundDepartureDateTo"] = inboundTo.ToString(DateFormat),
                ["currency"] = _currency
            };

            var response = await GetResponse<RyanairRoundTripFaresData>(RyanairRoundTripFaresData.UriPath, query);

            return response.Fares.Select(one => RyanairToLibTypesMapper.Map(one)).ToList();
        }

        private async Task<T> GetResponse<T>(string path, Dictionary<string, string> queryData)
        {
            var uri = GetUri(path, queryData);
            var response = await _client.GetAsync(uri);
            await CheckResponse(response);

            var json = await response.Content.ReadAsStringAsync();

            var parsedResponse = JsonSerializer.Deserialize<T>(json, _jsonOptions);
            if (parsedResponse == null)
            {
                throw new ApiRequestFailedException("Reponse data could not be parsed to the expected format", json);
            }

            return parsedResponse;
        }

        private Uri GetUri(string path, Dictionary<string, string> queryData)
        {
            var queryStrings = queryData.Select(one => 
                $"{HttpUtility.UrlEncode(one.Key)}={HttpUtility.UrlEncode(one.Value)}");
            var fullQuery = string.Join("&", queryStrings.ToArray());

            var builder = new UriBuilder(SchemeName, Host)
            {
                Path = path,
                Query = fullQuery
            };

            return builder.Uri;
        }

        private async Task CheckResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ApiRequestFailedException("Ryaniar API returned not response.");
            }

            if (!response.IsSuccessStatusCode)
            {
                var fullResponse = await response.Content.ReadAsStringAsync();
                throw new ApiRequestFailedException(
                    $"Ryanair API returned an unseccessfull status code. " +
                    $"Code: {response.StatusCode}, " +
                    $"Reason: {response.ReasonPhrase}", fullResponse);
            }
        }
    }
}
