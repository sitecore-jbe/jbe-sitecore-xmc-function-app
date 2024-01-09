using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System;
using Sitecore.ContentHub;
using Sitecore.XMC.WebhookRequest;

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

            //Get Content Hub OAuth Client Bearer Token
            Sitecore.ContentHub.OAuthToken contentHubOAuthToken = await Sitecore.ContentHub.OAuthToken.GetOAuthTokenAsync(
                requestParameters.TenantUrl,
                requestParameters.Username,
                requestParameters.Password,
                requestParameters.ContentHubClientId,
                requestParameters.ContentHubClientSecret,
                log);
            var contentHubAccessToken = contentHubOAuthToken.AccessToken;

            //Get Entity Id
            EntityResource entityResource = await EntityResource.GetEntityResourceAsync(
                requestParameters.TenantUrl,
                contentHubAccessToken,
                entityIdentifier,
                log);
            var entityId = entityResource.Id;

            //Get Sitecore XMC OAuth Client Bearer Token
            Sitecore.XMC.OAuthToken xmcOAuthToken = await Sitecore.XMC.OAuthToken.GetOAuthTokenAsync(
                requestParameters.XMCClientId,
                requestParameters.XMCClientSecret,
                log);
            var xmcAccessToken = xmcOAuthToken.AccessToken;

            //Compose ContentHub Url
            var rawValue =
             requestParameters.TenantUrl.EndsWith("/")
                ? requestParameters.TenantUrl + "en-us/ch-products/ch-productssearch/ch-productdetails2/" + entityId + "?tab28428=Details"
                : requestParameters.TenantUrl + "/en-us/ch-products/ch-productssearch/ch-productdetails2/" + entityId + "?tab28428=Details";

            //Update XMC Item with ContentHub Url
            GraphQL.UpdateItemResult item = await Item.UpdateItemAsync(
                            requestParameters.XMCTenantUrl,
                            xmcAccessToken,
                            webHookEvent.Item.Id,
                            rawValue,
                            log);
            var itemId = item.Data.UpdateItem.Item.ItemId;

            string responseMessage = string.IsNullOrEmpty(itemId)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {itemId}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

    }
}
