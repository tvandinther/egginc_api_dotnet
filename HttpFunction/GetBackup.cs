using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HttpFunction
{
    public class GetBackup
    {
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

            var firstContactResponse = await RequestForwarder.AuxbrainRequest<FirstContactResponse>("first_contact", firstContactRequest);
            await response.WriteAsync(firstContactResponse.FirstContact.Backup.ToJSON());
        }
    }
}