using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;

namespace SecuredFunction
{
    public static class FunctionAPI
    {
        [FunctionName("Sample")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log, ClaimsPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated)
            {
                return new UnauthorizedObjectResult("You're not authenticated.");
            }
            if (!principal.Claims.Any(e => e.Type == "roles" && e.Value.ToLower() == "readwrite.all"))
            {
                return new UnauthorizedObjectResult("You're unauthorized, please, check if you're in role 'Applications' with 'ReadWrite.All' audience.");
            }

            string responseMessage = $"Hello, {principal.Identity.Name}. This HTTP triggered function executed successfully.";
            var roles = principal.Claims.Where(e => e.Type == "roles").Select(e => e.Value);
            foreach (var role in roles)
            {
                responseMessage += $"|{role}";
            }
            return new OkObjectResult(responseMessage);
        }
    }
}
