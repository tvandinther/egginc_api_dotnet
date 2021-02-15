using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HttpFunction.Exceptions;
using Microsoft.AspNetCore.Http;

namespace HttpFunction.Endpoints
{
    public static class GetCoopStatus
    {
        private const string Endpoint = "coop_status";
        
        public static async Task Handle(HttpRequest request, HttpResponse response)
        {
            var body = new StreamReader(request.Body);
            var requestString = await body.ReadToEndAsync();
            var collection = HttpUtility.ParseQueryString(requestString);

            var contractId = collection.Get("contractId");
            var coopId = collection.Get("coopId");

            if (contractId is null || coopId is null)
            {
                throw new BadRequestException();
            }
            
            var coopStatusRequest = new CoopStatusRequest{
                ClientVersion = Globals.ClientVersion,
                ContractId = contractId,
                CoopId = coopId
            };

            try
            {
                var coopStatusResponse = await RequestForwarder.AuxbrainRequest<CoopStatusResponse>(Endpoint, coopStatusRequest);
                await response.WriteAsync(coopStatusResponse.CoopStatus.ToJson());
            }
            catch (HttpRequestException)
            {
                throw new NotFoundException();
            }
        }
    }
}