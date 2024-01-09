using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System;

namespace Sitecore.XMC
{
    public static class GetContentHubEntityId
    {
        [FunctionName("GetContentHubEntityId")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            //Example Request Parameters
            //{ "tenantUrl":"https://almu-llbg.sitecoresandbox.cloud", "username": "JBESitecoreXMC", "password": "S!t3c0r3", "clientId": "JBESitecoreXMC", "clientSecret": "S!t3c0r3", "entityIdentifier": "Gs8f4QkTs0WEyYCdT-GPAg"}

            //Structure Request Parameters
            var requestParameters = RequestParameters.Initialize(requestBody, req.Query, log);

            //Structure Webhook Event
            var webHookEvent = WebHookEvent.Initialize(requestBody, log);

            //Get the Content Hub identifier of the item in the WebHook 
            var entityIdentifier = webHookEvent.Item.SharedFields.Single(e => e.Id == Guid.Parse("{9b0343a9-9f69-4e0f-a059-9215bc8fe422}")).Value;

            //Get OAuth Client Bearer Token
            OAuthToken oAuthToken = await OAuthToken.GetOAuthTokenAsync(
                requestParameters.TenantUrl,
                requestParameters.Username,
                requestParameters.Password,
                requestParameters.ClientId,
                requestParameters.ClientSecret,
                log);
            var accessToken = oAuthToken.AccessToken;

            //Get Entity Id
            EntityResource entityResource = await EntityResource.GetEntityResourceAsync(
                requestParameters.TenantUrl,
                accessToken,
                entityIdentifier,
                log);
            var entityId = entityResource.Id;

            string responseMessage = string.IsNullOrEmpty(entityId)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {entityId}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

    }
}
