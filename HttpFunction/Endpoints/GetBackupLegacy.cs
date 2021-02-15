using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HttpFunction.Exceptions;
using Microsoft.AspNetCore.Http;

namespace HttpFunction.Endpoints
{
    public static class GetBackupLegacy
    {
        private const string Endpoint = "first_contact";
        
        public static async Task Handle(HttpRequest request, HttpResponse response)
        {
            var body = new StreamReader(request.Body);
            var requestString = await body.ReadToEndAsync();
            var collection = HttpUtility.ParseQueryString(requestString);
            
            var firstContactRequest = new FirstContactRequest
            {
                ClientVersion = Globals.ClientVersion,
                DeviceId = Globals.DeviceId,
                GameServicesId = collection.Get("id")
            };

            try
            {
                var firstContactResponse = await RequestForwarder.AuxbrainRequest<FirstContactResponse>(Endpoint, firstContactRequest);
                var backup = firstContactResponse?.FirstContact?.Backup;
                
                if (backup is null)
                {
                    throw new NotFoundException();
                    
                }
                
                await response.WriteAsync(backup.ToJson());
            }
            catch (HttpRequestException)
            {
                throw new NotFoundException();
            }
        }
    }
}