using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSS
{
    internal class test
    {
        //private async Task<bool> CheckForDOMBasedXSS(string baseUrl, string payload)
        //{
        //    try
        //    {
        //        string originalPayload = payload;
        //        await DownloadBrowser();

        //        using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
        //        using (var page = await browser.NewPageAsync())
        //        {
        //            string urlWithPayload = $"{baseUrl}?query={originalPayload}";

        //            await page.GoToAsync(urlWithPayload);

        //            // Wait for page to load and execute client-side scripts
        //            await Task.Delay(1000); // Adjust the timeout as needed

        //            string responseBody = await page.GetContentAsync();

        //            if (IsXSSDetected(responseBody, originalPayload))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error while fetching the URL: {ex.Message}");
        //    }

        //    return false;
        //}


        //private static async Task DownloadBrowser()
        //{
        //    var browserFetcher = new BrowserFetcher();
        //    var revisionInfo = await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

        //    Console.WriteLine($"Downloaded browser revision: {revisionInfo.Revision}");
        //}
    }
}
