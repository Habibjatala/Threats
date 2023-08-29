//using System;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace RCETester
//{
//    class Program
//    {
//        static async Task Main(string[] args)
//        {
//            Console.WriteLine("Enter the URL to test:");
//            string url = Console.ReadLine();
//            Console.WriteLine("......................................\n");
//            Console.WriteLine("Welcome to RCE Testing Application");
//            Console.WriteLine("........................................");
//            Console.WriteLine("........................................");

//            Console.WriteLine("The testing process is starting...");
//            Console.WriteLine("........................................\n\n");


//            RCETester tester = new RCETester();
//            await tester.TestRCE(url);

//            Console.WriteLine("\n\nTesting complete.");
//        }
//    }

//    internal class RCETester
//    {
//        private readonly HttpClient httpClient = new HttpClient();
//        private readonly string payload = "your_payload_here"; // Replace with your RCE payload

//        public async Task TestRCE(string url)
//        {
//            try
//            {
//                string fullUrl = $"{url}?input={Uri.EscapeDataString(payload)}";

//                using (var response = await httpClient.GetAsync(fullUrl))
//                {
//                    if (response.IsSuccessStatusCode)
//                    {
//                        string responseBody = await response.Content.ReadAsStringAsync();

//                        // Check if the payload output is present in the response
//                        if (responseBody.Contains(payload))
//                        {
//                            Console.WriteLine($"Potential RCE vulnerability found on {url}");
//                            Console.WriteLine("Response:");
//                            Console.WriteLine(responseBody);
//                        }
//                        else
//                        {
//                            Console.WriteLine($"No obvious RCE vulnerability detected on {url}");
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine($"HTTP Error: {(int)response.StatusCode} - {response.ReasonPhrase}");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error while testing RCE: {ex.Message}");
//            }
//        }
//    }
//}