using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace XSS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the URL to test:");
            string url = Console.ReadLine();
            Console.WriteLine("......................................\n");

            if (string.IsNullOrEmpty(url))
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            if (!IsValidUrl(url))
            {
                Console.WriteLine("Invalid URL format");
                return;
            }

            AdvancedSSRFTester tester = new AdvancedSSRFTester();
            await tester.TestSSRF(url);

            Console.WriteLine("\n\nTesting complete.");
        }

        private static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }

    internal class AdvancedSSRFTester
    {
        private readonly HttpClient httpClient = new HttpClient();

        public async Task TestSSRF(string baseUrl)
        {
            string[] testUrls = {
                "http://example.com",
                "http://localhost",
                "http://127.0.0.1",
                "http://internal-server-ip"
                // Add more test URLs here
            };

            Console.WriteLine("Testing process is starting...");
            Console.WriteLine("\n...................................");



            using (var reportFile = new System.IO.StreamWriter("Advanced_SSRF_Report.txt"))
            {
                foreach (string testUrl in testUrls)
                {
                    bool isVulnerable = await CheckForSSRF(baseUrl, testUrl);
                    string result = isVulnerable ? $"Possible SSRF Vulnerability Found with test URL: {testUrl}" : $"No obvious vulnerability detected with test URL: {testUrl}";
                    Console.WriteLine(result);
                    reportFile.WriteLine(result);
                }
            }
        }

        private async Task<bool> CheckForSSRF(string baseUrl, string testUrl)
        {
            try
            {
                string urlToTest = $"{baseUrl}?url={Uri.EscapeDataString(testUrl)}";

                using (var response = await httpClient.GetAsync(urlToTest))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Detect SSRF by checking for a specific keyword in the response
                        if (responseBody.Contains("internal server data") || responseBody.Contains("sensitive data"))
                        {
                            return true;
                        }

                        // Check response headers for indications of SSRF (e.g., "Server" header)
                        if (response.Headers.Contains("Server") && response.Headers.GetValues("Server").Contains("InternalServer"))
                        {
                            return true;
                        }

                        // Perform more advanced detection methods here

                        // Example of an advanced detection method: 
                        // Analyze the DNS resolution of the test URL and compare with the resolved IP
                        IPAddress resolvedIpAddress = await ResolveIpAddress(testUrl);
                        if (resolvedIpAddress != null)
                        {
                            IPAddress serverIpAddress = await ResolveIpAddress(baseUrl);
                            if (resolvedIpAddress.Equals(serverIpAddress))
                            {
                                return true; // Detected SSRF
                            }
                        }

                        // If SSRF is detected, return true
                        // return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while testing the URL: {ex.Message}");
            }

            return false;
        }

        private async Task<IPAddress> ResolveIpAddress(string url)
        {
            try
            {
                IPHostEntry hostEntry = await Dns.GetHostEntryAsync(url);
                return hostEntry.AddressList[0];
            }
            catch
            {
                return null;
            }
        }
    }
}



