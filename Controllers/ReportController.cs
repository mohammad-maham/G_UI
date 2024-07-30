using G_APIs.Models;
using System.Web.Mvc;

namespace G_APIs.Controllers
{

    public class ReportController : Controller
    {

        [GoldAuthorize]
        public ActionResult Report1(Report model)
        {
            return View(model ?? new Report());
        }

        [GoldAuthorize]
        public ActionResult Report2(Report model)
        {
            return View(model ?? new Report());
        }

        [GoldAuthorize]
        public ActionResult Report3(Report model)
        {
            return View(model ?? new Report());
        }
    }
}
