using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GSA.Search.WebApi.Logging;

namespace GSA.Search.WebApi.Handlers
{
    public class UsageHandler : DelegatingHandler
    {

        

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            var apiRequest = new WebApiUsageRequest(request);

            apiRequest.Content = request.Content.ReadAsStringAsync().Result;

            

            LogManager.LogInfo(apiRequest);

            return base.SendAsync(request, cancellationToken).ContinueWith(
          task =>
          {
              var apiResponse = new WebApiUsageResponse(task.Result);
              apiResponse.Content = task.Result.Content.ReadAsStringAsync().Result;

              LogManager.LogInfo(apiResponse);

              return task.Result;
          }, cancellationToken);
        }
        
    }
}