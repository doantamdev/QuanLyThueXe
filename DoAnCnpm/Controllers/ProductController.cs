using DoAnCnpm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System.Net;
using DoAnCnpm.Models.CommentViewModal;

namespace DoAnCnpm.Controllers
{
    public class ProductController : Controller
    {
        DoAnPMEntities database = new DoAnPMEntities();
        // GET: Product
        public ActionResult Index(string _name, int? page, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            // Kích thước trang
            int pageSize = 8;
            int pageNum = (page ?? 1);
            IQueryable<Product> productList = database.Products;

            //Lấy danh sách từng sản phẩm theo danh mục
            List<Product> Xe2Banhs = database.Products.Where(p => p.Category == "001").Take(5).ToList();
            List<Product> Xe4Banhs = database.Products.Where(p => p.Category == "002").Take(5).ToList();
            List<Product> XeKhachs = database.Products.Where(p => p.Category == "003").Take(5).ToList();
            List<Product> XeDaps = database.Products.Where(p => p.Category == "1236").Take(5).ToList();
            List<Product> XeVanChuyens = database.Products.Where(p => p.Category == "1523").Take(5).ToList();

            var listProduct = new ListProduct {
                Xe2Banh = Xe2Banhs,
                Xe4Banh = Xe4Banhs,
                XeKhach = XeKhachs,
                XeDap = XeDaps,
                XeVanChuyen = XeVanChuyens
            };

            // Lọc theo tên sản phẩm
            if (!string.IsNullOrEmpty(_name))
            {
                productList = productList.Where(p => p.NamePro.Contains(_name));
            }

            // Lọc theo giá
            productList = productList.Where(p => p.Price >= min && p.Price <= max);

            // Sắp xếp theo tên sản phẩm
            productList = productList.OrderByDescending(p => p.NamePro);

            return View(listProduct);
        }

        public ActionResult Create()
        {
            List<Category> list = database.Categories.ToList();
            ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", "");
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
            List<Category> list = database.Categories.ToList();
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
                ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", 1);
                database.Products.Add(pro);
                database.SaveChanges();
                return RedirectToAction("QuanLyXe");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var findPro = database.Products.Find(id);

            if (findPro == null)
            {
              
                return HttpNotFound();
            }

           
            List<Category> list = database.Categories.ToList();

            
            ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", findPro.Category);

            return View(findPro);
        }
        [HttpPost]
        public ActionResult Edit(int id, Product pro)
        {
            try
            {
                // Find the existing product by ID
                var existingProduct = database.Products.Find(id);

                if (existingProduct == null)
                {
                    // Handle the case where the product with the given ID doesn't exist
                    return HttpNotFound();
                }

                // Update the product properties
                existingProduct.NamePro = pro.NamePro;
                existingProduct.Price = pro.Price;
                existingProduct.Category = pro.Category;
                existingProduct.DecriptionPro = pro.DecriptionPro;
                existingProduct.Quantity = pro.Quantity;
                existingProduct.MauXe = pro.MauXe;
                existingProduct.Vitri = pro.Vitri;

                if (pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    existingProduct.ImagePro = "~/Content/images/" + filename;
                    pro.UploadImage.SaveAs(Server.MapPath("~/Content/images/" + filename));
                }
                List<Category> list = database.Categories.ToList();

                ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", existingProduct.Category);

                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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


        public ActionResult ProductList(int id, int? page, string _name, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            //Kích thước trang
            int pageSize = 8;
            int pageNum = (page ?? 1);
            IQueryable<Product> products = database.Products.Where(p => p.Category1.Id == id);

            var cate = database.Categories.FirstOrDefault(c => c.Id == id);
            Session["cateName"] = cate.NameCate;

            // Lọc theo tên sản phẩm
            if (!string.IsNullOrEmpty(_name))
            {
                products = products.Where(p => p.NamePro.Contains(_name));
            }

            // Lọc theo giá
            products = products.Where(p => p.Price >= min && p.Price <= max);

            // Sắp xếp theo tên sản phẩm
            products = products.OrderByDescending(p => p.NamePro);

            return View(products.ToPagedList(pageNum, pageSize));
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
        public ActionResult ProductDetails(int id)
        {
            // Lấy thông tin sản phẩm từ database và truyền nó đến View
            Product product = database.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult AddComment(CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                // Lấy giá trị UserName từ phiên làm việc
                var userName = Session["UserCus"] as string;
                var userID = Session["UserId"] as int?;


                Comment newComment = new Comment
                {
                    CommentMsg = comment.CommentMsg,
                    CommentDate = DateTime.Now,
                    XeId = comment.ProductID,
                    UserName = userName,
                    UserID = userID,
                    Rate = comment.Rate
                };
                database.Comments.Add(newComment);
                database.SaveChanges();

            }

            return RedirectToAction("Details", new { id = comment.ProductID });
        }



        public ActionResult GetComments(int productId)
        {
            var comments = database.Comments.Where(c => c.XeId == productId).ToList();
            return PartialView("_CommentsPartial", comments);
        }
        [HttpPost]
        public ActionResult DeleteComment(int commentId)
        {
            // Lấy giá trị UserID từ phiên làm việc
            var userId = Session["UserId"] as int?;

            // Lấy ProductID của bình luận trước khi xóa
            var productID = database.Comments.Where(c => c.ID == commentId).Select(c => c.XeId).FirstOrDefault();

            // Kiểm tra xem người dùng có quyền xóa bình luận hay không
            Comment cmt = database.Comments.FirstOrDefault(c => c.ID == commentId && c.UserID == userId);

            if (cmt != null)
            {
                database.Comments.Remove(cmt);
                database.SaveChanges();

                // Nếu xóa thành công, trả về một JSON object báo cáo xóa thành công
                return Json(new { success = true, productId = productID });
            }

            // Nếu không có quyền hoặc xóa không thành công, trả về một JSON object báo cáo không thành công
            return Json(new { success = false });
        }

    }
}