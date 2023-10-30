using DoAnCnpm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCnpm.Controllers
{
    public class HomeController : Controller
    {
        DoAnPMEntities context = new DoAnPMEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult ITestGD()
        //{
        //    List<OrderDetail> orderDetail = context.OrderDetails.ToList();
        //    List<Product> proList = context.Products.ToList();

        //    var query = from od in orderDetail
        //                join p in proList on od.IDProduct equals p.ProductID into tbl
        //                group od by new
        //                {
        //                    idPro = od.IDProduct,
        //                    namePro = od.Product.NamePro,
        //                    imagePro = od.Product.ImagePro,
        //                    price = od.Product.Price
        //                }
        //                into gr
        //                orderby gr.Sum(s => s.Quantity) descending
        //                select new ViewModel
        //                {
        //                    IDPro = gr.Key.idPro,
        //                    NamePro = gr.Key.namePro,
        //                    ImgPro = gr.Key.imagePro,
        //                    pricePro = (decimal)gr.Key.price,
        //                    sum_Quantity = gr.Sum(s => s.Quantity)
        //                };
        //    return View(query.Take(4).ToList());
        //}
        [AllowAnonymous]
        public ActionResult TestAD()
        {
            return View();
        }

      public ActionResult GetData()
        {
            int thang8 = context.HighCharts.Where(x => x.ThoiGian == "Thang8").Count();
            int thang9 = context.HighCharts.Where(x => x.ThoiGian == "Thang9").Count();
            int thang10 = context.HighCharts.Where(x => x.ThoiGian == "Thang10").Count();
            Ratio obj = new Ratio();
            obj.Thang8 = thang8;
            obj.Thang9 = thang9;
            obj.Thang10 = thang10;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int Thang8 { get; set; }
            public int Thang9 { get; set; }
            public int Thang10 { get; set; }
        }


        public ActionResult TrangHome() {
            return View();
                }

    }
}