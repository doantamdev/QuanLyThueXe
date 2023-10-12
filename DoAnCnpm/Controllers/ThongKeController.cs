using DoAnCnpm.Models;
using System.Linq;
using System.Web.Mvc;

namespace DoAnCnpm.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly DoAnPMEntities context;

        public ThongKeController()
        {
            context = new DoAnPMEntities();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }

        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var query = context.OrderDetails
                .GroupBy(od => od.Product.NamePro)
                .Select(g => new { name = g.Key, count = g.Sum(od => od.Quantity) })
                .ToList();

            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}
