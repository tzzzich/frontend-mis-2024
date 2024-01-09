using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers
{
    [Route("/error/{action}")]
    public class ErrorController : Controller
    {
		[Route("/error/404")]
		public ActionResult PageNotFound()
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return View("NotFound");
        }
    }
}

