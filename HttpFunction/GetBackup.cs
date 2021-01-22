using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HttpFunction
{
    public class GetBackup
    {
        private const string Endpoint = "first_contact";
        
        public static async Task Handle(HttpRequest request, HttpResponse response)
        {
            var body = new StreamReader(request.Body);
            var requestString = await body.ReadToEndAsync();

            var firstContactRequest = new FirstContactRequest
            {
                ClientVersion = Globals.ClientVersion,
                DeviceId = Globals.DeviceId,
                EiUserId = requestString
            };

            var firstContactResponse = await RequestForwarder.AuxbrainRequest<FirstContactResponse>(Endpoint, firstContactRequest);
            await response.WriteAsync(firstContactResponse.FirstContact.Backup.ToJSON());
        }
    }
}