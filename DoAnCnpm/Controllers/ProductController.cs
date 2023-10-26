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