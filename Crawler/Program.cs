using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace Crawler
{
    public class Program
    {
        static ArrayList ExtractEmails(string data)
        {

            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            MatchCollection emailMatches = emailRegex.Matches(data);

            var result = new ArrayList();
            foreach (Match emailMatch in emailMatches)
            {
                if (!result.Contains(emailMatch.Value))
                {
                    result.Add(emailMatch.Value);
                };

            }
            return result;
        }


        public static async Task Main(string[] args)
        {




            try
            {
                if (args.Length == 0)
                {
                    throw new ArgumentNullException("URL");
                }
                string url = args[0];

                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    throw new ArgumentException("Niepoprawny URL");
                }

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(url);
                httpClient.Dispose();
                string responseContent = await response.Content.ReadAsStringAsync();
                ArrayList emails = ExtractEmails(responseContent);

                if (emails.Count > 0)
                {
                    foreach (string e in emails)
                    {
                        Console.WriteLine(e);

                    }
                }else { Console.WriteLine("Nie znaleziono żadnego adresu email"); }


            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Błąd w czasie pobierania strony");
            }




        }
    }
}