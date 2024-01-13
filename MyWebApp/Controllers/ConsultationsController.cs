using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Models.ViewModels;
using MyWebApp.Utils;
using System.Net;

namespace MyWebApp.Controllers
{
    public class ConsultationsController : Controller
    {
        public async Task<IActionResult> Index(InspectionsFilter filter)
        {
            var client = this.GetHttpClient();
            var parameters = UriParamsParser.ParsFromeObjectFields(filter);
            var model = new PatientAndInspectionsForViewModel();
            model.Filter = filter;
            var response = await client.GetAsync($"consultation{parameters}");
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var inspections = await response.Content.ReadFromJsonAsync<InspectionPagedListModel>();
                model.Inspections = new InspectionListForViewModel(inspections, filter.Grouped.GetValueOrDefault());
                if (filter.Grouped.GetValueOrDefault())
                {
                    int i = await this.GetChildInspections(model.Inspections.Inspections, HttpContext.Session.GetString("accessToken"));
                }
            }
            else
            {
                return await this.GetErrorResult(response);
            }
            response = await client.GetAsync("dictionary/icd10/roots");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                model.Icd10RootsList = await response.Content.ReadFromJsonAsync<List<Icd10RecordModel>>();
            }
            else
            {
                return await this.GetErrorResult(response);
            }
            return View("InspectionsListForConsultationsView", model);
        }

        private async Task<int> GetChildInspections(IEnumerable<InspectionListItemForViewModel> inspections, string authToken)
        {
            var client = this.GetHttpClient(authToken);
            foreach (var inspection in inspections)
            {
                if (inspection.Inspection.HasChain)
                {
                    var response = await client.GetAsync($"inspection/{inspection.Inspection.Id}/chain");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var innerInspections = await response.Content.ReadFromJsonAsync<IEnumerable<InspectionPreviewModel>>();
                        var innerList = new List<InspectionPreviewModel>();
                        var addedList = new List<InspectionPreviewModel>();
                        foreach (var item in innerInspections)
                        {
                            if (item.PreviousId == inspection.Inspection.Id)
                                inspection.InnerInspections.Add(new InspectionListItemForViewModel(item));
                            else
                            {
                                innerList.Add(item);
                            }
                        }
                        var addedDict = new Dictionary<Guid, List<InspectionListItemForViewModel>>();
                        foreach (var item in innerList)
                        {
                            var parent = inspection.InnerInspections.Find(ins => ins.Inspection.Id == item.PreviousId);
                            if (parent != null)
                            {
                                var element = new InspectionListItemForViewModel(item);
                                parent.InnerInspections.Add(element);
                                addedList.Add(item);
                                addedDict.Add(item.Id, parent.InnerInspections);
                            }
                        }
                        foreach (var item in addedList) innerList.Remove(item);
                        while (innerList.Count > 0)
                        {
                            addedList.Clear();
                            foreach (var item in innerList)
                            {
                                if (addedDict.ContainsKey(item.PreviousId.GetValueOrDefault()))
                                {
                                    var list = addedDict[item.PreviousId.GetValueOrDefault()];
                                    list.Add(new InspectionListItemForViewModel(item));
                                    addedDict.Add(item.Id, list);
                                    addedList.Add(item);
                                }
                            }
                            foreach (var item in addedList) innerList.Remove(item);
                        }
                        int i = await this.GetChildInspections(inspection.InnerInspections, authToken);
                    }
                    else
                    {
                        var respMes = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return 1;
        }
    }
}
