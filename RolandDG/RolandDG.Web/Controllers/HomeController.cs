using System.Web.Mvc;

namespace RolandDG.Web.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(CacheProfile = "LongLived")]
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(CacheProfile = "LongLived")]
        public ActionResult About()
        {
            ViewBag.Message = "Application inspired by Roland Digital Group";

            return View();
        }

        [OutputCache(CacheProfile = "LongLived")]
        public ActionResult Contact()
        {
            ViewBag.Message = "You want to contact me! Uaou!";

            return View();
        }
    }
}