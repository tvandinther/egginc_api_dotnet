using System.Net;
using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HttpFunction
{
    public class Function : IHttpFunction
    {
        public async Task HandleAsync(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            // CORS
            response.Headers.Append("Access-Control-Allow-Origin", "*");
            if (HttpMethods.IsOptions(request.Method))
            {
                response.Headers.Append("Access-Control-Allow-Methods", "POST, GET, OPTIONS");
                response.Headers.Append("Access-Control-Allow-Headers", "Content-Type");
                response.Headers.Append("Access-Control-Max-Age", "3600");
                response.StatusCode = (int) HttpStatusCode.NoContent;
                return;
            }
            
            switch (request.Path)
            {
                case "/get_backup":
                {
                    await GetBackup.Handle(request, response);
                    break;
                }
                case "/get_backup_legacy":
                {
                    await GetBackupLegacy.Handle(request, response);
                    break;
                }
                case "/get_contracts":
                {
                    await GetContracts.Handle(request, response);
                    break;
                }
                case "/get_periodicals":
                {
                    await GetPeriodicals.Handle(request, response);
                    break;
                }
                case "/get_coop_status":
                {
                    await GetCoopStatus.Handle(request, response);
                    break;
                }
                default:
                {
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;
                }
            }
        }
    }
}