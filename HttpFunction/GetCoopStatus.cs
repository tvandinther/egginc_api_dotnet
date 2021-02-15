using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace HttpFunction
{
    public static class GetCoopStatus
    {
        private const string Endpoint = "coop_status";
        
        public static async Task Handle(HttpRequest request, HttpResponse response)
        {
            var body = new StreamReader(request.Body);
            var requestString = await body.ReadToEndAsync();
            var collection = HttpUtility.ParseQueryString(requestString);

            var coopStatusRequest = new CoopStatusRequest{
                ClientVersion = Globals.ClientVersion,
                ContractId = collection.Get("contractId"),
                CoopId = collection.Get("coopId")
            };

            try
            {
                var coopStatusResponse = await RequestForwarder.AuxbrainRequest<CoopStatusResponse>(Endpoint, coopStatusRequest);
                await response.WriteAsync(coopStatusResponse.CoopStatus.ToJson());
            }
            catch (HttpRequestException)
            {
                response.StatusCode = (int) HttpStatusCode.NotFound;
                await response.CompleteAsync();
            }
        }
    }
}