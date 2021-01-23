using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace HttpFunction
{
    public static class GetBackupLegacy
    {
        private const string Endpoint = "first_contact";
        
        public static async Task Handle(HttpRequest request, HttpResponse response)
        {
            var body = new StreamReader(request.Body);
            var requestString = await body.ReadToEndAsync();
            var collection = HttpUtility.ParseQueryString(requestString);
            
            Console.WriteLine(collection.Get("id"));
            var firstContactRequest = new FirstContactRequest
            {
                ClientVersion = Globals.ClientVersion,
                DeviceId = Globals.DeviceId,
                GameServicesId = collection.Get("id")
            };

            var firstContactResponse = await RequestForwarder.AuxbrainRequest<FirstContactResponse>(Endpoint, firstContactRequest);
            Console.WriteLine(firstContactResponse.FirstContact.Backup.UserName);
            await response.WriteAsync(firstContactResponse.FirstContact.Backup.ToJson());
        }
    }
}