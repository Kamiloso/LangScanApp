using LangScan.Configuration;
using Microsoft.AspNetCore.Builder;
using System.Linq;
using System.Threading.Tasks;

namespace LangScan;

public static class Startup
{
    public static async Task Start(this WebApplication app, string[] args)
    {
        bool isTestRun = args.Contains("--test-run");
        bool isIntegrationTestRun = args.Contains("--run-it-tests");

        if (!isTestRun)
        {
            if (isIntegrationTestRun)
            {
                // TODO: Wait a few seconds, then run integration tests (all on another thread)
            }

            await app.ApplyPostgresMigrationsAsync();
            app.Run();
        }
    }
}
