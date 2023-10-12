using DoAnCnpm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCnpm.Controllers
{
    public class OrderDetailController : Controller
    {
        DoAnPMEntities database = new DoAnPMEntities();
        // GET: OrderDetail
        public ActionResult GroupByTop3()
        {
            List<OrderDetail> orderDetail = database.OrderDetails.ToList();
            List<Product> proList = database.Products.ToList();

            var query = from od in orderDetail
                        join p in proList on od.IDProduct equals p.ProductID into tbl
                        group od by new
                        {
                            idPro = od.IDProduct,
                            namePro = od.Product.NamePro,
                            imagePro = od.Product.ImagePro,
                            price = od.Product.Price
                        }
                        into gr
                        orderby gr.Sum(s => s.Quantity) descending
                        select new ViewModel
                        {
                            IDPro = gr.Key.idPro,
                            NamePro = gr.Key.namePro,
                            ImgPro = gr.Key.imagePro,
                            pricePro = (decimal)gr.Key.price,
                            sum_Quantity = gr.Sum(s => s.Quantity)
                        };
            return View(query.Take(1).ToList());
        }
    }
}