using Microsoft.AspNetCore.Mvc;
using MyWebApp.Models;
using MyWebApp.Utils;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net;

namespace MyWebApp.Controllers
{
    public class PatientController : Controller
    {
        [HttpGet]
        [Route("patient/{id}")]
        public async Task<IActionResult> GetPatient(string id, PatientInspectionsFilter filter)
        {
            var client = this.GetHttpClient();
            var patient = new PatientForViewModel();
            patient.Filter = filter;
            var response = await client.GetAsync($"patient/{id}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                patient.Patient = await response.Content.ReadFromJsonAsync<PatientModel>();
                var parameters = UriParamsParser.ParsFromeObjectFields(filter);
                response = await client.GetAsync($"patient/{id}/inspections{parameters}");
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    var inspections = await response.Content.ReadFromJsonAsync<InspectionPagedListModel>();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        patient.Inspections = new InspectionListForViewModel(inspections, filter.Grouped.GetValueOrDefault());
                        if (filter.Grouped.GetValueOrDefault())
                        { 
                            int i = await this.GetChildInspections(patient.Inspections.Inspections, HttpContext.Session.GetString("accessToken"));
                        }
                    }
                    else
                    {
                        return await this.GetErrorResult(response);
                    }
                }
                else
                {
                    return await this.GetErrorResult(response);
                }
                response = await client.GetAsync("dictionary/icd10/roots");
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    patient.Icd10RootsList = await response.Content.ReadFromJsonAsync<List<Icd10RecordModel>>();
                }
                else
                {
                    return await this.GetErrorResult(response);
                }
            }
            else
            {
                return await this.GetErrorResult(response);
            }
            patient.Inspections.IsDead = CheckIsDead(patient.Inspections);
            return View("Patient", patient);
        }

        [HttpPost]
        [Route("/patient/storedata")]
        public IActionResult StoreData(InspectionTransferModel model)
        {
            try
            {
                HttpContext.Session.SetString("inspectionTransfer", JsonConvert.SerializeObject(model));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private bool CheckIsDead(InspectionListForViewModel model)
        {
            if (model.IsDead) return model.IsDead;
            foreach(var item in model.Inspections)
            {
                foreach (var mediumItem in item.InnerInspections)
                {
                    model.IsDead = mediumItem.Inspection.Conclusion == "Death";
                    if (model.IsDead) return model.IsDead;
                    foreach(var innerItem in mediumItem.InnerInspections)
                    {
                        model.IsDead = innerItem.Inspection.Conclusion == "Death";
                        if (model.IsDead) return model.IsDead;

                    }
                }
            }
            return model.IsDead;
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
                            if(item.PreviousId==inspection.Inspection.Id)
                                inspection.InnerInspections.Add(new InspectionListItemForViewModel(item));
                            else
                            {
                                innerList.Add(item);
                            }
                        }
                        var addedDict = new Dictionary<Guid, List<InspectionListItemForViewModel>>();
                        foreach (var item in innerList)
                        {
                            var parent = inspection.InnerInspections.Find(ins=>ins.Inspection.Id == item.PreviousId);
                            if(parent!= null)
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
                            foreach(var item in innerList)
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
