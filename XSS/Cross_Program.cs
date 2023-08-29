//using PuppeteerSharp;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using XSS;

//namespace XSS
//{
//    class Cross_Program
//    {
//        static async Task Main(string[] args)
//        {
//            Console.WriteLine("Enter the URL to test:");
//            string url = Console.ReadLine();
//            Console.WriteLine("......................................\n");

//            if (string.IsNullOrEmpty(url))
//            {
//                Console.WriteLine("Invalid URL");
//                return;
//            }

//            if (!IsValidUrl(url))
//            {
//                Console.WriteLine("Invalid URL format");
//                return;
//            }


           
//            XSSChecker checker = new XSSChecker();
//            await checker.TestXSSPayloads(url);

//            Console.WriteLine("\n\nTesting complete.");
//        }

       


//        private static bool IsValidUrl(string url)
//        {
//            return Uri.TryCreate(url, UriKind.Absolute, out _);
//        }
//    }
//}