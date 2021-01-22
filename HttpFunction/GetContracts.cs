using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HttpFunction
{
    public class GetContracts
    {
        public static async Task Handle(HttpRequest request, HttpResponse response)
        {
            var firstContactRequest = new PeriodicalsRequest
            {
                ClientVersion = Globals.ClientVersion,
            };

            var periodicalsResponse = await RequestForwarder.AuxbrainRequest<PeriodicalsResponse>("get_periodicals", firstContactRequest);
            await response.WriteAsync(periodicalsResponse.Periodicals.Contracts.ToJSON());
        }
    }
}