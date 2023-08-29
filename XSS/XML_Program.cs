//using System;
//using System.IO;
//using System.Xml;

//namespace XSS
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string xmlData = @"<?xml version='1.0' encoding='UTF-8'?>
//                              <!DOCTYPE root [
//                                <!ENTITY xxe SYSTEM 'file:///etc/passwd'>
//                              ]>
//                              <root>&xxe;</root>";

//            // Feature: Content-Type and Mime-Type Checking
//            if (!IsXMLContentType(xmlData))
//            {
//                Console.WriteLine("Invalid XML content type.");
//                return;
//            }

//            // Feature: Secure Input Validation
//            if (!IsValidXMLInput(xmlData))
//            {
//                Console.WriteLine("Invalid XML input.");
//                return;
//            }

//            XmlReaderSettings settings = new XmlReaderSettings();

//            // Feature: Disable DTD Processing (Prevents XXE)
//            settings.DtdProcessing = DtdProcessing.Prohibit;

//            // Feature: Disable External Entity Resolution (Prevents XXE)
//            settings.XmlResolver = null;

//            // ... (rest of your code)

//            // Create the XMLReader instance
//            using (XmlReader reader = XmlReader.Create(new StringReader(xmlData), settings))
//            {
//                try
//                {
//                    while (reader.Read())
//                    {
//                        if (reader.NodeType == XmlNodeType.Element)
//                        {
//                            Console.WriteLine("Element: " + reader.Name);
//                        }
//                        else if (reader.NodeType == XmlNodeType.Text)
//                        {
//                            Console.WriteLine("Text: " + reader.Value);
//                        }
//                    }
//                }
//                catch (XmlException ex)
//                {
//                    Console.WriteLine("Error: " + ex.Message);
//                }
//            }
//        }

//        // Feature: Content-Type and Mime-Type Checking
//        static bool IsXMLContentType(string content)
//        {
//            // Check if the content starts with "<?xml"
//            return content.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase);
//        }

//        // Feature: Secure Input Validation
//        static bool IsValidXMLInput(string input)
//        {
//            // Basic validation: Check if input contains "ENTITY" (this should be more comprehensive in real scenarios)
//            return !input.Contains("ENTITY");
//        }
//    }
//}
////////////////////////////////////////////////////////////////////////////////////////////////////
///


//using System;
//using System.IO;
//using System.Net;
//using System.Xml;
//using System.Linq;


//namespace XSS
//{
//    class XML_Program
//    {
//        static void Main(string[] args)
//        {
//            string url = "https://app.zerotrusted.ai/#/"; // Replace with your desired URL

//            // Fetch HTML content from the URL
//            string htmlContent = GetHTMLContentFromUrl(url);

//            // Extract product title from HTML
//            string productTitle = ExtractProductTitle(htmlContent);

//            // Convert HTML content to XML-like format with sanitized product title as the root element name
//            string xmlData = WrapHTMLInXML(htmlContent, productTitle);

//            // ... (rest of your code)

//            // Feature: Content-Type and Mime-Type Checking
//            if (!IsXMLContentType(xmlData))
//            {
//                Console.WriteLine("Invalid XML content type.");
//                return;
//            }

//            // Feature: Secure Input Validation
//            if (!IsValidXMLInput(xmlData))
//            {
//                Console.WriteLine("Invalid XML input.");
//                return;
//            }

//            XmlReaderSettings settings = new XmlReaderSettings();

//            // Feature: Disable DTD Processing (Prevents XXE)
//            settings.DtdProcessing = DtdProcessing.Prohibit;

//            // Feature: Disable External Entity Resolution (Prevents XXE)
//            settings.XmlResolver = null;

//            Console.WriteLine("XML processing started...");

//            bool hasDetectedXXE = false; // Flag to track XXE detection

//            // Create the XMLReader instance
//            using (XmlReader reader = XmlReader.Create(new StringReader(xmlData), settings))
//            {
//                try
//                {
//                    while (reader.Read())
//                    {
//                        if (reader.NodeType == XmlNodeType.Element)
//                        {
//                            Console.WriteLine(".............................................");
//                            Console.WriteLine("Element: " + reader.Name); // Display the element name

//                            // Check for XXE-related patterns in the element name
//                            if (IsXXEPatternDetected(reader.Name))
//                            {
//                                hasDetectedXXE = true;
//                            }
//                        }
//                        else if (reader.NodeType == XmlNodeType.Text)
//                        {
//                            //Console.WriteLine("......................................................................");
//                            Console.WriteLine("......................................................................");
//                            Console.WriteLine("Text: " + reader.Value);
//                        }
//                    }

//                    if (hasDetectedXXE)
//                    {
//                        Console.WriteLine("......................................................................");
//                        Console.WriteLine("......................................................................");
//                        Console.WriteLine("XML processing completed with potential XXE detection.");
//                    }
//                    else
//                    {

//                        Console.WriteLine("......................................................................");
//                        Console.WriteLine("XML processing completed successfully without XXE detection.");
//                        Console.WriteLine("......................................................................");

//                    }
//                }
//                catch (XmlException ex)
//                {
//                    Console.WriteLine("...........................................................");
//                    Console.WriteLine("Error: " + ex.Message);
//                }
//            }
//        }

//        // Extract product title from HTML content using simple pattern matching
//        static string ExtractProductTitle(string htmlContent)
//        {
//            // Try to find the title within the HTML content
//            int titleStart = htmlContent.IndexOf("<title>", StringComparison.OrdinalIgnoreCase);
//            int titleEnd = htmlContent.IndexOf("</title>", StringComparison.OrdinalIgnoreCase);

//            if (titleStart >= 0 && titleEnd > titleStart)
//            {
//                string title = htmlContent.Substring(titleStart + 7, titleEnd - titleStart - 7).Trim();
//                return title;
//            }

//            // Fallback value if title extraction fails
//            return "UnknownProductTitle";
//        }

//        // Wrap HTML content in an XML-like format with sanitized product title as the root element name
//        static string WrapHTMLInXML(string htmlContent, string rootElementName)
//        {
//            // Remove special characters and spaces from the root element name
//            string sanitizedRootElementName = new string(rootElementName.Where(c => char.IsLetterOrDigit(c)).ToArray());

//            // Ensure that the sanitized root element name is not empty
//            if (string.IsNullOrEmpty(sanitizedRootElementName))
//            {
//                sanitizedRootElementName = "UnknownRootElement";
//            }

//            return $@"<?xml version='1.0' encoding='UTF-8'?>
//            <{sanitizedRootElementName}><![CDATA[
//            {htmlContent}
//            ]]></{sanitizedRootElementName}>";
//        }

//        // Check for XXE-related patterns in the element name
//        static bool IsXXEPatternDetected(string elementName)
//        {
//            // You can implement more comprehensive checks here
//            return elementName.Contains("ENTITY");
//        }

//        // Feature: Content-Type and Mime-Type Checking
//        static bool IsXMLContentType(string content)
//        {
//            // Check if the content starts with "<?xml"
//            return content.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase);
//        }

//        // Feature: Secure Input Validation
//        static bool IsValidXMLInput(string input)
//        {
//            // Basic validation: Check if input contains "ENTITY" (this should be more comprehensive in real scenarios)
//            return !input.Contains("ENTITY");
//        }

//        // Fetch HTML content from a URL
//        static string GetHTMLContentFromUrl(string url)
//        {
//            using (WebClient client = new WebClient())
//            {
//                // Add a user agent header to mimic a browser request
//                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");

//                return client.DownloadString(url);
//            }
//        }
//    }
//}

