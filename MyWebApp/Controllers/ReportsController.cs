using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Utils;

namespace MyWebApp.Controllers
{
    public class ReportsController : Controller
    {
        public async Task<IActionResult> Index(IcdRootsReportFiltersModel filter)
        {
            if(filter.Start==null){
                filter.Start = DateTime.Now.AddDays(-1);
            }
            if (filter.End == null)
            {
                filter.End = DateTime.Now;
            }
            
            string parameters = $"?start={filter.Start.GetValueOrDefault().ToString("yyyy-MM-ddThh:mm:ss")}&end={filter.End.GetValueOrDefault().ToString("yyyy-MM-ddThh:mm:ss")}";

            if(filter.IcdRoots.Count > 0)
            {
                foreach(var root in filter.IcdRoots)
                {
                    parameters += $"&IcdRoots={root}";
                }
            }
            ViewBag.Filter = filter;
            var client = this.GetHttpClient();

            var response = await client.GetAsync($"report/icdrootsreport{parameters}");
            if (filter.Start > filter.End)
            {
                ViewBag.Errors = Problem(detail: "Начальная дата не может быть больше конечной", statusCode: 400);
                response = await client.GetAsync("dictionary/icd10/roots");
                List<Icd10RecordModel> roots = await response.Content.ReadFromJsonAsync<List<Icd10RecordModel>>();
                ViewBag.Roots = roots;
                return View("Reports");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var body = await response.Content.ReadFromJsonAsync<IcdRootsReportModel>();
                response = await client.GetAsync("dictionary/icd10/roots");
                List<Icd10RecordModel> roots = await response.Content.ReadFromJsonAsync<List<Icd10RecordModel>>();
                ViewBag.Roots = roots;
                return View("Reports", body);
            }
            else
            {
                var errMesage = await this.GetErrorResult(response);
                ViewBag.Errors = errMesage;
                response = await client.GetAsync("dictionary/icd10/roots");
                List<Icd10RecordModel> roots = await response.Content.ReadFromJsonAsync<List<Icd10RecordModel>>();
                ViewBag.Roots = roots;
                return View("Reports");
            }
        }
    }
}
