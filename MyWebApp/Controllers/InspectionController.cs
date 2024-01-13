using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyWebApp.Models;
using MyWebApp.Models.ViewModels;
using MyWebApp.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Web.Helpers;

namespace MyWebApp.Controllers
{
    public class InspectionController : Controller
    {

        [HttpGet]
        [Route("/inspection/{id}")]
        public async Task<IActionResult> GetInspection(Guid id)
        {
            if (HttpContext.Session.Keys.Contains<string>("id"))
            {
                ViewData["CurrUserId"] = HttpContext.Session.GetString("id");
            }
            else
            {
                return Redirect("/login");
            }
            var client = this.GetHttpClient();
            var response = await client.GetAsync($"inspection/{id.ToString()}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var inspection = await response.Content.ReadFromJsonAsync<InspectionModel>();
                ViewBag.Diagnoses = inspection.Diagnoses;
                return View("InspectionDetail", inspection);
            }
            else
            {
                return await this.GetErrorResult(response, true);
            }
        }

        [HttpGet]
        [Route("/inspection/getcomments/{consultId}")]
        public async Task<IActionResult> GetComments(string consultId)
        {
            var client = this.GetHttpClient();
            var response = await client.GetAsync($"consultation/{consultId}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var consultation = await response.Content.ReadFromJsonAsync<ConsultationModel>();
                var currComments = consultation.Comments.Where(c => c.ParentId == consultation.Comments.Where(rc => rc.ParentId == null).Select(x => x.Id).FirstOrDefault()).ToList();
                List<CommentItemForViewModel> model = new List<CommentItemForViewModel>();
                foreach (var comment in currComments)
                {
                    model.Add(new CommentItemForViewModel
                    {
                        Comment = comment,
                        level = 0
                    });
                }
                ProcessComments(model, consultation.Comments);
                return View("CommentsListView", new CommentsListForViewModel { Comments = model, ConsultId = consultId, UserId = HttpContext.Session.GetString("id") });
            }
            else
            {
                return await this.GetErrorResult(response);
            }
        }

        protected void ProcessComments(IEnumerable<CommentItemForViewModel> comments, IEnumerable<CommentModel> rawCommentsList)
        {
            foreach (var comment in comments)
            {
                var currComments = rawCommentsList.Where(c => c.ParentId == comment.Comment.Id).ToList();
                if (currComments.Count > 0)
                {
                    foreach (var commentModel in currComments)
                    {
                        var item = new CommentItemForViewModel();
                        item.Comment = commentModel;
                        item.level = comment.level + 1;
                        comment.ChildComments.Add(item);
                    }
                    ProcessComments(comment.ChildComments, rawCommentsList);
                }
            }
        }

        [HttpPost]
        [Route("/Inspection/AddComment")]
        public async Task<IActionResult> AddComment(CommentAddFromViewModel data)
        {
            var client = this.GetHttpClient();
            var response = await client.PostAsJsonAsync($"consultation/{data.ConsultId}/comment", new CommentCreateModel { Content = data.Content, ParentId = data.ParentId });
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok();
            }
            else
            {
                return await this.GetErrorResult(response);
            }
        }

        [HttpPut]
        [Route("/Inspection/EditComment")]
        public async Task<IActionResult> EditComment(CommentEditFromViewModel data)
        {
            var client = this.GetHttpClient();
            var response = await client.PutAsJsonAsync($"consultation/comment/{data.CommentId}", new InspectionCommentCreateModel { Content = data.Content });
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok();
            }
            else
            {
                return await this.GetErrorResult(response);
            }
        }


        [HttpGet]
        [Route("/inspection/create")]
        public async Task<IActionResult> Create()
        {
            InspectionTransferModel transferData;
            string? prevId = null;
            bool isSecond = false;
            if (HttpContext.Session.Keys.Contains<string>("inspectionTransfer"))
            {
                string transferSring = HttpContext.Session.GetString("inspectionTransfer");
                transferData = JsonConvert.DeserializeObject<InspectionTransferModel>(transferSring);
                prevId = transferData.ParentId.ToString();
                isSecond = transferData.IsSecondary.GetValueOrDefault();
            }
            else
            {
                throw new ArgumentNullException("InspectionTransferModel is absent in current session context");
            }
            var model = new InspectionCreateModel { Date = DateTime.Now.TruncateToMinutes() };
            ViewBag.IsSecond = isSecond;
            var diagList = new List<DiagnosisModel>();
            ViewBag.Diagnoses = diagList;
            model.PreviousInspectionId = prevId != "" ? prevId : null;
            var client = this.GetHttpClient();
            var response = await client.GetAsync($"patient/{transferData.PatientId}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                ViewBag.Patient = await response.Content.ReadFromJsonAsync<PatientModel>();
            }
            else
            {
                return await this.GetErrorResult(response);
            }
            response = await client.GetAsync($"patient/{transferData.PatientId}/inspections?grouped=false&page=1&size=1000");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                ViewBag.PrevionsList = (await response.Content.ReadFromJsonAsync<InspectionPagedListModel>()).Inspections.Where(x => !x.HasChain || !x.HasNested).Select(i =>
                    new { Id = i.Id, Name = $"Дата: {i.Date.ToString("dd.MM.yyyy hh:mm")} - Код диагноза: {i.Diagnosis.Code}" }).ToList();
            }
            else
            {
                return await this.GetErrorResult(response);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InspectionCreateModel model)
        {
            if (ModelState.IsValid)
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
                var client = this.GetHttpClient();
                var response = await client.PostAsJsonAsync($"patient/{transferData.PatientId}/inspections", model);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }
                else
                {
                    return await this.GetErrorResult(response);
                }
            }
            else
            {
                return await this.GetValidationErrorResult(ModelState);
            }

        }

        [HttpGet]
        [Route("/inspection/addconsultation/{position}")]
        public async Task<IActionResult> AddConsultation(int position)
        {
            var client = this.GetHttpClient();
            var response = await client.GetAsync("dictionary/speciality?page=1&size=1000");
            var model = new ConsultationItemCreateForViwModel();
            model.Position = position;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                model.Specialities = (await response.Content.ReadFromJsonAsync<SpecialtiesPagedListModel>()).Specialties;
            }
            else
            {
                return await this.GetErrorResult(response);
            }
            return View("Consultation", model);
        }

        [HttpGet]
        [Route("/inspection/adddiagnosis/{diagCount}")]
        public async Task<IActionResult> AddDiagnosis(int diagCount)
        {
            var client = this.GetHttpClient();
            var response = await client.GetAsync("dictionary/icd10/roots");
            ViewBag.DiagPosition = diagCount;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                ViewBag.DiagnosisList = await response.Content.ReadFromJsonAsync<IEnumerable<Icd10RecordModel>>();
            }
            else
            {
                return await this.GetErrorResult(response);
            }
            return View("Diagnosis", new DiagnosisCreateModel());
        }

        [HttpGet]
        [Route("/inspection/getdiaglevellist")]
        public async Task<IActionResult> GetDiagLevelList([FromQuery] int level, [FromQuery] int position, [FromQuery] string ParentDiag)
        {
            var parentCodes = ParentDiag.Trim().Split("-");
            var diagnoses = new List<Icd10RecordModel>();
            var client = this.GetHttpClient();
            HttpResponseMessage response;
            if (parentCodes.Length > 1)
            {
                string firstFirstLetter = parentCodes[0][0].ToString();
                string secondFirstLetter = parentCodes[1][0].ToString();
                string firstFirstDigt = parentCodes[0][1].ToString();
                string secondFirstDigt = parentCodes[1][1].ToString();
                if (firstFirstLetter == secondFirstLetter)
                {
                    if (firstFirstDigt == secondFirstDigt)
                    {
                        for (int i = 0; i < 10; i++)
                        {

                            response = await client.GetAsync($"dictionary/icd10?request={firstFirstLetter}{firstFirstDigt}{i}&page=1&size=1000");
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var records = (await response.Content.ReadFromJsonAsync<Icd10SearchModel>()).Records;
                                if (records.Count > 0) diagnoses.Add(records[0]);
                            }
                            else
                            {
                                return await this.GetErrorResult(response);
                            }
                        }
                    }
                    else if ((Int32.Parse(secondFirstDigt) - Int32.Parse(firstFirstDigt)) == 1)
                    {
                        for (int i = Int32.Parse(firstFirstDigt); i < 10; i++)
                        {

                            response = await client.GetAsync($"dictionary/icd10?request={firstFirstLetter}{firstFirstDigt}{i}&page=1&size=1000");
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var records = (await response.Content.ReadFromJsonAsync<Icd10SearchModel>()).Records;
                                if (records.Count > 0) diagnoses.Add(records[0]);
                            }
                            else
                            {
                                return await this.GetErrorResult(response);
                            }
                        }
                        for (int i = 0; i < Int32.Parse(secondFirstDigt) + 1; i++)
                        {

                            response = await client.GetAsync($"dictionary/icd10?request={firstFirstLetter}{secondFirstDigt}{i}&page=1&size=1000");
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var records = (await response.Content.ReadFromJsonAsync<Icd10SearchModel>()).Records;
                                if (records.Count > 0) diagnoses.Add(records[0]);
                            }
                            else
                            {
                                return await this.GetErrorResult(response);
                            }
                        }
                    }
                    else
                    {

                        for (int i = Int32.Parse(firstFirstDigt); i < Int32.Parse(secondFirstDigt) + 1; i++)
                        {

                            response = await client.GetAsync($"dictionary/icd10?request={firstFirstLetter}{i}0-{firstFirstLetter}&page=1&size=1000");
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var records = (await response.Content.ReadFromJsonAsync<Icd10SearchModel>()).Records;
                                if (records.Count > 0) diagnoses.Add(records[0]);
                            }
                            else
                            {
                                return await this.GetErrorResult(response);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = Int32.Parse(firstFirstDigt); i < 10; i++)
                    {

                        response = await client.GetAsync($"dictionary/icd10?request={firstFirstLetter}{i}0-{firstFirstLetter}&page=1&size=1000");
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var records = (await response.Content.ReadFromJsonAsync<Icd10SearchModel>()).Records;
                            if (records.Count > 0) diagnoses.Add(records[0]);
                        }
                        else
                        {
                            return await this.GetErrorResult(response);
                        }
                    }
                    for (int i = 0; i < Int32.Parse(secondFirstDigt) + 1; i++)
                    {

                        response = await client.GetAsync($"dictionary/icd10?request={secondFirstLetter}{i}0-{secondFirstLetter}&page=1&size=1000");
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var records = (await response.Content.ReadFromJsonAsync<Icd10SearchModel>()).Records;
                            if (records.Count > 0) diagnoses.Add(records[0]);
                        }
                        else
                        {
                            return await this.GetErrorResult(response);
                        }
                    }
                }

            }
            else if (parentCodes.Length > 0 && !parentCodes[0].Contains('.'))
            {
                response = await client.GetAsync($"dictionary/icd10?request={parentCodes[0][0]}{parentCodes[0][1]}{parentCodes[0][2]}&page=1&size=1000");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var records = (await response.Content.ReadFromJsonAsync<Icd10SearchModel>()).Records;
                    records = records.Where(r => r.Code.Contains('.')).ToList();
                    if (records.Count > 0) diagnoses.AddRange(records);
                    else return Ok();
                }
                else
                {
                    return await this.GetErrorResult(response);
                }
            }
            else
            {
                return Ok();
            }
            return View("DiagnosisLevelSelect", new DiagnosisLevelListModel { Diagnoses = diagnoses, Level = level + 1, Position = position });
        }


        [HttpGet]
        public async Task<IActionResult> GetInspectionEditModal(string id)
        {
            var client = this.GetHttpClient();
            var response = await client.GetAsync($"inspection/{id}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var inspection = await response.Content.ReadFromJsonAsync<InspectionModel>();
                var diagList = new List<DiagItemForEditView>();
                foreach (var diag in inspection.Diagnoses)
                {
                    var icd10Item = (await (await client.GetAsync($"dictionary/icd10?request={diag.Code}&page=1&size=1000"))
                                                            .Content.ReadFromJsonAsync<Icd10SearchModel>())
                                                                .Records.Where(r=>r.Name == diag.Name).FirstOrDefault();
                    diagList.Add(new DiagItemForEditView { Diagnosis = diag, Icd10Record = icd10Item });
                }
                ViewBag.Diagnoses = diagList;
                ViewBag.InspectionId = inspection.Id.ToString();
                var inspectionEditModel = new InspectionEditModel {
                    Anamnesis = inspection.Anamnesis,
                    Complaints = inspection.Complaints,
                    Treatment = inspection.Treatment, 
                    Conclusion = inspection.Conclusion, 
                    DeathDate = inspection.DeathDate.GetValueOrDefault().TruncateToMinutes(), 
                    NextVisitDate = inspection.NextVisitDate.GetValueOrDefault().TruncateToMinutes(),
                };
                return View("InspectionEdit", inspectionEditModel);
            }
            else
            {
                return await this.GetErrorResult(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, InspectionEditModel model)
        {
            if (ModelState.IsValid)
            {
                var client = this.GetHttpClient();
                var response = await client.PutAsJsonAsync($"inspection/{id}", model);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return Ok();
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return Redirect("/login");
                }
                else
                {
                    return await this.GetErrorResult(response);
                }
            }
            else
            {
                return await this.GetValidationErrorResult(ModelState);
            }
        }
    }
}
