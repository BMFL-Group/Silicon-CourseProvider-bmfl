using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using static HotChocolate.ErrorCodes;

namespace Silicon_CourseProvider_bmfl.Funtions
{
    public class Blazor_IA(ILogger<Blazor_IA> logger, IGraphQLRequestExecutor executor)
    {
        private readonly ILogger<Blazor_IA> _logger = logger;
        private readonly IGraphQLRequestExecutor _executor = executor;

        [Function("Blazor_IA")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "blazor_ia")] HttpRequest req)
        {
            return await _executor.ExecuteAsync(req);
        }
    }
}
