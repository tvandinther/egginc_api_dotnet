﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HttpFunction.Endpoints
{
    public static class GetPeriodicals
    {
        private const string Endpoint = "get_periodicals";
        
        public static async Task Handle(HttpRequest request, HttpResponse response)
        {
            var firstContactRequest = new PeriodicalsRequest
            {
                ClientVersion = Globals.ClientVersion,
            };

            var periodicalsResponse = await RequestForwarder.AuxbrainRequest<PeriodicalsResponse>(Endpoint, firstContactRequest);
            await response.WriteAsync(periodicalsResponse.Periodicals.ToJson());
        }
    }
}