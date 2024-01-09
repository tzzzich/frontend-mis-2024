using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Utils;
using Newtonsoft.Json;
using System.Net;
using System.Web.Helpers;

namespace MyWebApp.Controllers
{
    public class InspectionController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            InspectionTransferModel transferData;
            if (HttpContext.Session.Keys.Contains<string>("inspectionTransfer"))
            {
                string transferSring = HttpContext.Session.GetString("inspectionTransfer");
                transferData = JsonConvert.DeserializeObject<InspectionTransferModel>(transferSring);
            }
            else
            {
                throw new ArgumentNullException("InspectionTransferModel is absent in current session context");
            }
            var model = new InspectionCreateForViewModel();
            var client = this.GetHttpClient();
            var response = await client.GetAsync($"patient/{transferData.PatientId}");
            if(response.StatusCode == HttpStatusCode.OK) 
            {
                model.Patient = await response.Content.ReadFromJsonAsync<PatientModel>();
            }
            else
            {
                this.GetErrorResult(response);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InspectionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else
            {
                return await this.GetValidationErrorResult(ModelState);
            }
        
        }
    }
}
