using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace NgHive.Functions
{
    public static class SignalRNegotiate
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
        [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
        [SignalRConnectionInfo(HubName = "votes")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
    }
}