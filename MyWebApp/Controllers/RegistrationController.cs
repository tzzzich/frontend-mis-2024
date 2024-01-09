using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Utils;
using NuGet.Protocol;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;

namespace MyWebApp.Controllers
{
    public class RegistrationController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View("Registration");
        }

        [HttpPost]
        [Route("/registration/save")]
        public async Task<IActionResult> Save(DoctorRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var client = this.GetHttpClient();
                HttpResponseMessage response = await client.PostAsJsonAsync<DoctorRegisterModel>("doctor/register", model);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var token = await response.Content.ReadFromJsonAsync<TokenResponseModel>();
                    HttpContext.Session.SetString("accessToken", token.Token);
                    HttpContext.Session.SetString("name", model.Name);
                    return Ok();

                }
                else
                {

                    return await this.GetErrorResult(response);

                }
            }
            return await this.GetValidationErrorResult(ModelState);
        }

        [HttpGet]
        [Route("/registration/getspecialites")]
        public async Task<IActionResult> GetSpetialitiesList(string spec)
        {
            var client = this.GetHttpClient();
            HttpResponseMessage response = await client.GetAsync("dictionary/speciality?size=1000" + (spec!=null?$"&name={spec}":""));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var spesialitiesList = await response.Content.ReadFromJsonAsync<SpecialtiesPagedListModel>();
                return View("Specialites", spesialitiesList.Specialties);
            }
            else
            {
                return await this.GetErrorResult(response);
            }
        }
    }
}
