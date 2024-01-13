using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Utils;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using NuGet.Protocol;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using MyWebApp.Models.ViewModels;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(PatientsFilterModel filter)
        {
            var client = this.GetHttpClient();
            var parameters = UriParamsParser.ParsFromeObjectFields(filter);
            HttpResponseMessage response = await client.GetAsync($"patient{parameters}");
            ViewBag.Filter = filter;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var patientsList = await response.Content.ReadFromJsonAsync<PatientPagedListModel>();
				return View(patientsList);
			}
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Redirect("/login");
            }
            else
            {
                var respMes = await response.Content.ReadAsStringAsync();
                dynamic jobject = JObject.Parse(respMes);
                ViewBag.Error = jobject.Last.First.toString();
            }
            return View(new PatientPagedListModel() { Patients=new List<PatientModel>()});
        }

        [HttpPost]
        [Route("/home/createpatient")]
        public async Task<IActionResult> Save(PatientCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var client = this.GetHttpClient();
                HttpResponseMessage response = await client.PostAsJsonAsync<PatientCreateModel>("patient", model);
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

        [HttpGet]
        [Route("/home/getmodal")]
        public async Task<IActionResult> GetCreatePatientModal()
        {
              return View("PatientCreate", new PatientCreateModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}