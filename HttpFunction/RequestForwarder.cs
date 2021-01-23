using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Protobuf;

namespace HttpFunction
{
    public static class RequestForwarder
    {
        private const string TargetDomain = "http://afx-2-dot-auxbrainhome.appspot.com/ei";

        public static async Task<TResponse> AuxbrainRequest<TResponse>(string endpoint, IMessage requestMessage) where TResponse : IMessage<TResponse>, new()
        {
            using (var stream = new MemoryStream())
            {
                requestMessage.WriteTo(stream);
                var requestStringBase64 = Convert.ToBase64String(stream.ToArray());
                
                using (var client = new HttpClient())
                {
                    var url = $"{TargetDomain}/{endpoint}";

                    var formData = new Dictionary<string, string>
                    {
                        {"data", requestStringBase64}
                    };


                    using (var responseMessage = await client.PostAsync(url, new FormUrlEncodedContent(formData)))
                    {
                        responseMessage.EnsureSuccessStatusCode();
                        
                        using (var content = responseMessage.Content)
                        {
                            var base64ResponseString = await content.ReadAsStringAsync();
                            var responseStream = Convert.FromBase64String(base64ResponseString);
                            var parser = new MessageParser<TResponse>(() => new TResponse());
                            return parser.ParseFrom(responseStream);
                        }
                    }
                }
            }
        }
    }
}