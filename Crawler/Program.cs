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

            var result = new ArrayList(); // recommended 
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

            string url = args[0];
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string responseContent = await response.Content.ReadAsStringAsync();
            ArrayList emails = ExtractEmails(responseContent);

            foreach (string e in emails)
            {
                Console.WriteLine(e);

            }
            

        }
    }
}