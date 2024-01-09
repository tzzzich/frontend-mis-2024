using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Utils;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;

namespace MyWebApp.Controllers
{
	public class LoginController : Controller
	{
		// GET: LoginController
		public ActionResult Index()
		{
			return View("Login");
		}

		[HttpPost]
		[Route("/login/login")]
		public async Task<IActionResult> DoLogin (LoginCredentialsModel model)
		{
			if (ModelState.IsValid)
			{
                var client = this.GetHttpClient();
                HttpResponseMessage response = await client.PostAsJsonAsync("doctor/login", model);
				if(response.StatusCode== HttpStatusCode.OK)
				{
					response.StatusCode = HttpStatusCode.OK;
					var token = await response.Content.ReadFromJsonAsync<TokenResponseModel>();
					HttpContext.Session.SetString("accessToken", token.Token);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    response = await client.GetAsync("doctor/profile");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        try
                        {
                            var doctor = await response.Content.ReadFromJsonAsync<DoctorModel>();
                            HttpContext.Session.SetString("name", doctor.Name);
                            HttpContext.Session.SetString("id", doctor.Id);
                            return Ok();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
                        }
                    }

                    HttpContext.Session.SetString("name", "не удалось прочитать");
					return Ok();
				}
                else
                {

                    return await this.GetErrorResult(response);

                }
            }
            return await this.GetValidationErrorResult(ModelState);
        }


        [Route("/login/logout")]
        public async Task<IActionResult> Logout()
        {
            var client = this.GetHttpClient();
            HttpResponseMessage response = await client.PostAsync("doctor/login", null);
            HttpContext.Session.SetString("accessToken", "");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.StatusCode = HttpStatusCode.OK;
                var respModel = await response.Content.ReadFromJsonAsync<ResponseModel>();
                return Ok();
            }
            else
            {

                return await this.GetErrorResult(response);

            }

        }
	}
}
