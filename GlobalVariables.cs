using System.Net.Http.Headers;

namespace MusicForAllAdmin
{
    public class GlobalVariables
    {
        // HttpCliet provide a class to send/receive Http request/response
        public static HttpClient client = new HttpClient();          // Create an object of HttpClient

        static GlobalVariables()
        {
            client.BaseAddress = new Uri("https://localhost:7022/api/");        // REST API To consume
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}