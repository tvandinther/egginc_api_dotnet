using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HttpFunction
{
    public class GetCoopStatus
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

            var coopStatusResponse = await RequestForwarder.AuxbrainRequest<CoopStatusResponse>(Endpoint, coopStatusRequest);
            await response.WriteAsync(coopStatusResponse.CoopStatus.ToJSON());
        }
    }

    class CoopStatusRequestJson
    {
        public string ContractId;
        public string CoopId;
    }
}