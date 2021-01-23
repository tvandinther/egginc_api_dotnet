﻿using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace HttpFunction
{
    public static class GetBackup
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
                EiUserId = collection.Get("id")
            };

            var firstContactResponse = await RequestForwarder.AuxbrainRequest<FirstContactResponse>(Endpoint, firstContactRequest);
            await response.WriteAsync(firstContactResponse.FirstContact.Backup.ToJson());
        }
    }
}