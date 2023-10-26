using DoAnCnpm.Models;
using DoAnCnpm.Models.Payment;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoAnCnpm.Controllers
{
    public class ShoppingCartController : Controller
    {
        DoAnPMEntities database = new DoAnPMEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

    /*    public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart item = Session["Cart"] as Cart;
            return View(item);
        }*/

        //Action Tạo mới giỏ hàng
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        //Action thêm product vào giỏ hàng
        public ActionResult AddToCart(int id)
        {
            //Lấy product theo ID
            var product = database.Products.SingleOrDefault(s => s.ProductID == id);
            if (product != null)
            {
                GetCart().Add_Product_Cart(product);
            }
            return RedirectToAction("showCart", "ShoppingCart");
        }

        //Cập nhật số lượng
        public ActionResult UpdateCartQuantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int ID = int.Parse(form["IDProduct"]);
            DateTime ngayTra = DateTime.Parse(form["DateTra"]);
            int Quantity = (int)(ngayTra - DateTime.Today).TotalDays;
            cart.Update_quantity(ID, Quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");

        }

        //Xoá sản phẩm
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        //Icon giỏ hàng
        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_quantity_item = cart.Total_quantity();
            ViewBag.QuantityCart = total_quantity_item;
            return PartialView("BagCart");
        }

        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;


                //Bảng hoá đơn sản phẩm
                OrderPro order = new OrderPro();
                order.DateOrder = DateTime.Now;
                //order.OrderDate = DateTime.ParseExact(form["DateTra"], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                order.AddressDeliverry = form["AddressDelivery"];
                order.IDCus = int.Parse(form["CodeCustomer"]);
                order.TypePayment = 1;
                Customer cs = new Customer();
                order.PhoneCusNonAccount = cs.PhoneCus;
                database.OrderProes.Add(order);

                foreach (var item in cart.Items)
                {
                    //Lưu dòng sản phẩm vào bảng Chi tiết Hoá đơn
                    OrderDetail detail = new OrderDetail();
                    detail.IDOrder = order.ID;
                    detail.IDProduct = item.product.ProductID;
                    detail.UnitPrice = (double)item.product.Price;
                    detail.Quantity = item.quantity;
                    database.OrderDetails.Add(detail);

                    // -- Xử lý cập nhật lại số lượng tồn trong bảng Product -- //
                    //Lấy ID Product đang có trong giỏ hàng
                    foreach (var p in database.Products.Where(s => s.ProductID == detail.IDProduct))
                    {
                        //Số lượng tồn mới = Số lượng tồn - Số đã mua
                        var updateQuantity = p.Quantity - item.quantity;
                        //Thực hiện cập nhật lại số lượng tồn cho cột Quantity của bảng Product
                        p.Quantity = updateQuantity;;
                    }
                }
                database.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch
            {
                return Content("Lỗi thanh toán - Xin kiểm tra thông tin khách hàng...Xin cảm ơn.");
            }
        }


        //Thanh toán thành công
        public ActionResult CheckOut_Success()
        {
            return View();
        }

        public ActionResult QuanLyDonHang()
        {
            return View(database.OrderProes.ToList());
        }

        public ActionResult XuatFileExcel()
        {
            return View(database.OrderProes.ToList());
        }
        public ActionResult Delete(int id)
        {
            return View(database.OrderProes.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, OrderPro order)
        {
            try
            {
                order = database.OrderProes.Where(s => s.ID == id).FirstOrDefault();
                database.OrderProes.Remove(order);
                database.SaveChanges();
                return RedirectToAction("QuanLyDonHang");
            }
            catch
            {
                return Content("Không xóa được");
            }
        }


        public ActionResult Edit(int id)
        {
            return View(database.OrderProes.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, OrderPro order)
        {
            database.Entry(order).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("QuanLyDonHang");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new
               HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro order = database.OrderProes.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = database.OrderProes.Find(id);
            if (item != null)
            {
                database.OrderProes.Attach(item);
                item.TypePayment = trangthai;
                database.Entry(item).Property(x => x.TypePayment).IsModified = true;
                database.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "Unsuccess", Success = false });
        }
        private decimal CalculateDiscountAmount(string voucherCode)
        {
            // Sử dụng đối tượng DoAnCNPMEntities để truy vấn cơ sở dữ liệu và tính toán giảm giá dựa trên mã voucher
            Voucher voucher = database.Vouchers.FirstOrDefault(v => v.Code == voucherCode);

            if (voucher != null)
            {
                // Trả về giá trị giảm giá dựa trên thông tin voucher
                return voucher.DiscountAmount;
            }

            // Trả về null nếu không tìm thấy mã voucher hoặc không có giảm giá
            return -1;
        }

        public ActionResult ShowCart(string vouncher)
        {
            if (Session["Cart"] == null)
                return View("EmptyCart");
            // Lấy đối tượng giỏ hàng từ session
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                // Xử lý trường hợp giỏ hàng rỗng
                // Trả về view thông báo hoặc làm điều gì đó khác tùy theo yêu cầu của bạn
            }

            // Xử lý mã voucher và tính toán giảm giá dựa trên mã voucher (vouncher)
            decimal discountAmount = CalculateDiscountAmount(vouncher);

            // Kiểm tra nếu giá trị discountAmount là -1, hiển thị thông báo không tìm thấy voucher
            if (discountAmount == -1)
            {
                ViewBag.VoucherMessage = "Không tìm thấy voucher.";
                return View(cart);
            }

            // Áp dụng giảm giá cho từng sản phẩm trong giỏ hàng
            foreach (var item in cart.Items)
            {
                item.product.Price -= discountAmount;
            }

            // Lưu lại giỏ hàng đã cập nhật vào session
            Session["Cart"] = cart;

            // Tính tổng tiền sau khi áp dụng giảm giá

            // Trả về view hiển thị giỏ hàng với giảm giá và tổng tiền đã áp dụng
            ViewBag.DiscountAmount = discountAmount;

            return View(cart);
        }


    }
} 