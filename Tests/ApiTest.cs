using Microsoft.Playwright;
using NUnit.Framework;
using Serilog;
using TransferMarktTestFramework.Utilities;

namespace TransferMarktTestFramework.Tests
{
    public class ApiTest : BaseTest
    {
        [Test]
        public async Task No500ErrorInApiRequests()
        {
            Log.Information("Starting test: No500ErrorInApiRequests");
            Log.Information($"Navigating to: {_settings.BaseUrl}");
            await _page.GotoAsync(_settings.BaseUrl);
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Log.Information("Page has loaded.Checking for API errors.");
            
            bool foundError = false;

            _page.Response += (sender, response) =>
            {
                if (response.Status == (int)HttpStatusCode.InternalServerError)
                {
                    Log.Fatal("500 Internal Server Error detected in API request. ");
                    foundError = true;
                }
            };

            await _page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight);");
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle); 
            Log.Information("Scrolled to the bottom of the page. Waiting for API responses.");


            Assert.That(foundError, Is.False, "500 error detected in API requests.");
            Log.Information("Test completed successfully: No 500 error in API requests.");
        }
    }
}