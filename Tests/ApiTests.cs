using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TransferMarktTestFramework.Tests
{
    public class ApiTests : BaseTest
    {
        [Test]
        public async Task No500ErrorInApiRequests()
        {
            await _page.GotoAsync("https://www.transfermarkt.com/");

            var responses = await _page.WaitForResponseAsync(response =>
            {
                return response.Status >= 400; // Capture any error responses
            });

            if (responses.Status == 500)
            {
                Assert.Fail($"500 error detected in API request to {responses.Url}");
            }

            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
    }
}