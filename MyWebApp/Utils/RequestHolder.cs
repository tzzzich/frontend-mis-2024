using System.Net.Http.Headers;

namespace MyWebApp.Utils
{
    public class RequestHolder
    {
        public static HttpClient GetHttpClient(HttpContext context)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://mis-api.kreosoft.space/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var autorization = context.Session.GetString("accessToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", autorization);
            return client;
        }
    }
}
