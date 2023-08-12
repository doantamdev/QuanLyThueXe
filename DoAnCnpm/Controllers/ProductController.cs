using DoAnCnpm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace DoAnCnpm.Controllers
{
    public class ProductController : Controller
    {
        DoAnCNPMEntities database = new DoAnCNPMEntities();
        // GET: Product
        public ActionResult Index(string _name, int? page, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            // Kích thước trang
            int pageSize = 8;
            int pageNum = (page ?? 1);
            IQueryable<Product> productList = database.Products;

            // Lọc theo tên sản phẩm
            if (!string.IsNullOrEmpty(_name))
            {
                productList = productList.Where(p => p.NamePro.Contains(_name));
            }

            // Lọc theo giá
            productList = productList.Where(p => p.Price >= min && p.Price <= max);

            // Sắp xếp theo tên sản phẩm
            productList = productList.OrderByDescending(p => p.NamePro);

            return View(productList.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Create()
        {
            Product pro = new Product();
            return View(pro);
        }
        public ActionResult SelectCate()
        {
            Category se_cate = new Category();
            se_cate.ListCate = database.Categories.ToList<Category>();
            return PartialView(se_cate);
        }
        [HttpPost]

        public ActionResult Create(Product pro)
        {
            try
            {
                if (pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    pro.ImagePro = "~/Content/images/" + filename;
                    pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.Products.Add(pro);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            database.Entry(pro).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            try
            {
                pro = database.Products.Where(s => s.ProductID == id).FirstOrDefault();
                database.Products.Remove(pro);
                database.SaveChanges();
                return RedirectToAction("index");
            }
            catch
            {
                return Content("Không xóa được");
            }
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult QuanLyXe(string _name)
        {
            var products = database.Products.ToList();
            if (!string.IsNullOrEmpty(_name))
            {
                products = products.Where(p => p.NamePro.Contains(_name)).ToList();
            }

            return View(products);
        }


        public ActionResult GetProductsByCateId(int id, int? page)
        {
            //Kích thước trang
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var products = database.Products.Where(p => p.Category1.Id == id).ToList();
            return View("Index", products.ToPagedList(pageNum, pageSize));
        }

        public ActionResult GetProductsByCategory()
        {
            var categories = database.Categories.ToList();
            return PartialView("CategoriesPartialView", categories);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new
               HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = database.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


    }
}