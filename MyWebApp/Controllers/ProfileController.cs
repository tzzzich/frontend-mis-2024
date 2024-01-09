using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Reflection;
using MyWebApp.Models;
using NuGet.Protocol;
using MyWebApp.Utils;

namespace MyWebApp.Controllers
{
    public class ProfileController : Controller
    {
        public async Task<IActionResult> Index()
        {
            DoctorModel? model = null;
            if (ModelState.IsValid)
            {
                var client = this.GetHttpClient();
                HttpResponseMessage response = await client.GetAsync("doctor/profile");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Response.StatusCode = 200;
                    model = await response.Content.ReadFromJsonAsync<DoctorModel>();
                    return View("Profile", model);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Response.StatusCode = 401;
                    HttpContext.Session.SetString("accessToken", "");
                    return Redirect("/login");
                }
                Response.StatusCode = (int)response.StatusCode;
                return View("Profile", model);
            }
            return View("Profile", model);
        }

        [HttpPost]
        [Route("/profile/save")]
        public async Task<IActionResult> Save(DoctorEditModel model)
        {
            if (ModelState.IsValid)
            {
                var client = this.GetHttpClient();
                HttpResponseMessage response = await client.PutAsJsonAsync<DoctorEditModel>("doctor/profile", model);
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    return Ok();
                }
                else
                {

                    return await this.GetErrorResult(response);

                }
            }
            return await this.GetValidationErrorResult(ModelState);
        }
    }
}
