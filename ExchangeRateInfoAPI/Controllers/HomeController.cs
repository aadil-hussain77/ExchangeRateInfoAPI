using System.Web.Mvc;

namespace ExchangeRateInfoAPI.Controllers
{ 
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
