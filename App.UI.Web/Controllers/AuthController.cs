using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace App.UI.Web.Controllers
{
    public class AuthController : Controller
    {
        public async Task<IActionResult> Index()
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://localhost:5122/v1/Login");
            request.Method = HttpMethod.Post;

            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("User-Agent", "Thunder Client (https://www.thunderclient.com)");

            var bodyString = "{\r  \"email\": \"meselmias@gmail.com\",\r  \"password\": \"1234\"\r}";
            var content = new StringContent(bodyString, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);


            return View();
        }
    }
}
