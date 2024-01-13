using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyWebApp.Models;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Helpers;

namespace MyWebApp.Utils
{
    public static class ControllerHelper
    {
        public static HttpClient GetHttpClient(this Controller controller, string authToken = null)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://mis-api.kreosoft.space/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string autorization = null;
            try
            {
                autorization = controller.HttpContext.Session.GetString("accessToken");
            }
            catch
            {
                if (authToken != null)
                    autorization = authToken;
                else
                    throw new ArgumentNullException("AcessToken is absent");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", autorization);
            return client;
        }

        public static async Task<IActionResult> GetValidationErrorResult(this Controller controller, ModelStateDictionary modelState)
        {
            var notValidResponse = new ResponseModel();
            notValidResponse.Message = await controller.RenderViewToStringAsync("validationError", modelState.Values);
            return controller.BadRequest(notValidResponse);
        }

        public static async Task<IActionResult> GetErrorResult(this Controller controller, HttpResponseMessage response, bool returnView = false)
        {
            try
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return controller.Redirect("/login");
                }
                var resp = await response.Content.ReadFromJsonAsync<ResponseModel>();
                if (returnView)
                {
                    controller.Response.StatusCode = (int)response.StatusCode;
                    return controller.View("DefaultError", resp.Message);
                }
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                        return controller.Forbid(resp.Message);
                    case HttpStatusCode.BadRequest:
                        return controller.BadRequest(resp);
                    case HttpStatusCode.NotFound:
                        return controller.NotFound(resp);
                    default:
                        return controller.Problem(detail: resp.Message, statusCode: (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                var resp = await response.Content.ReadAsStringAsync();
                try
                {
                    dynamic jobject = JObject.Parse(resp);
                    dynamic errors = jobject["errors"];
                    var respModel = new ResponseModel();
                    respModel.Message = ErrorParser(errors);
                    respModel.Status = "400"; ;
                    return controller.BadRequest(respModel);
                }
                catch (Exception e)
                {
                    if (returnView)
                    {
                        controller.Response.StatusCode = (int)response.StatusCode;
                        return controller.View("DefaultError", $"{resp} {e.Message}");
                    }
                    return controller.Problem(detail: $"{resp}<br/>{e.Message}", statusCode: (int)response.StatusCode);

                }
            }

        }

        public static string ErrorParser(JToken obj)
        {
            if (obj.Type == JTokenType.String) return $"<span>{(string)obj.Value<string>()}</span><br/>";
            string result = "";
            foreach(var item in obj)
            {
                result += ErrorParser(item);
            }
            return result;
        }
    }
}
