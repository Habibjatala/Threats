using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XSS
{
    internal class XSSChecker
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly List<string> testPayloads = new List<string>
        {
            "<script>alert('XSS')</script>",
            "<img src=x onerror=alert('XSS')>",
            // Add more payloads here
        };

        private readonly List<string> payloadContexts = new List<string>
        {
            "<img src=\"x\" onerror=\"alert('XSS')\">",
            "<a href=\"javascript:alert('XSS')\">"
            // Add more payloads with different contexts here
        };

        public async Task TestXSSPayloads(string baseUrl)
        {
            using (var reportFile = new System.IO.StreamWriter("XSS_Report.txt"))
            {
                foreach (string payload in testPayloads)
                {
                    bool isVulnerable = await CheckForXSS(baseUrl, payload);
                    string result = isVulnerable ? $"Possible XSS Vulnerability Found with payload: {payload}" : $"No obvious vulnerability detected with payload: {payload}";
                    Console.WriteLine(result);
                    reportFile.WriteLine(result);
                }
                Console.WriteLine("............................................");
                Console.WriteLine("............................................");
                foreach (string payload in payloadContexts)
                {
                    bool isVulnerable = await CheckForXSS(baseUrl, payload);
                    string result = isVulnerable ? $"Possible XSS Vulnerability Found with payload in context: {payload}" : $"No obvious vulnerability detected with payload in context: {payload}";
                    Console.WriteLine(result);
                    reportFile.WriteLine(result);
                }


                
            }
        }

        private async Task<bool> CheckForXSS(string baseUrl, string payload)
        {
            try
            {
                string urlWithPayload = $"{baseUrl}?query={payload}";

                using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, urlWithPayload))
                {
                    httpRequestMessage.Headers.Add("Content-Security-Policy", "script-src 'self'");
                    using (var response = await httpClient.SendAsync(httpRequestMessage))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (IsXSSDetected(responseBody, payload))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching the URL: {ex.Message}");
            }

            return false;
        }

        private string EncodePayload(string payload, string encoding)
        {
            byte[] encodedBytes = Encoding.GetEncoding(encoding).GetBytes(payload);
            string encodedPayload = Uri.EscapeDataString(Encoding.GetEncoding(encoding).GetString(encodedBytes));
            return encodedPayload;
        }

        

        private bool IsXSSDetected(string responseBody, string payload)
        {
            string xssPattern = @"<[^>]*script.*?>|<[^>]*img[^>]*onerror.*?>";
            if (Regex.IsMatch(responseBody, xssPattern, RegexOptions.IgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}


