using DoAnCnpm.Models;
using DoAnCnpm.Models.VNPay;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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
        public void Payment(double totalMoney)
        {
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (totalMoney * 100).ToString()); // Nhân cho 100 để thêm 2 số 0 :) 
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); // Mã Website (Terminal ID)

            //Add Params of 2.1.0 Version
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            Response.Redirect(paymentUrl);
        }

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
                        p.Quantity = updateQuantity; ;
                    }
                }

                Payment(Convert.ToDouble(cart.Total_money()));
                VNPayReturn vNPayReturn = new VNPayReturn();
                vNPayReturn.ProcessResponses();
                if (vNPayReturn.TransacStatus == 0)
                {
                    database.SaveChanges();
                    cart.ClearCart();

                    return RedirectToAction("CheckOut_Success", "ShoppingCart", vNPayReturn);
                }

                return RedirectToAction("PaymentStatus", "Payment", vNPayReturn);
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

            if (Session["UserCus"] == null)
            {
                return RedirectToAction("Authen", "Login");
            }

            var cus = Session["ProfileCus"] as Customer;
            if (cus.Gioitinh != 0)
            {
                ViewBag.VoucherMessage = "Chỉ khách hàng nữ mới được sử dụng phiếu giảm giá.";
                return View(cart);
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
        public ActionResult CheckVoucher(string vouncher)
        {
            // Xử lý mã voucher và tính toán giảm giá dựa trên mã voucher (vouncher)
            decimal discountAmount = CalculateDiscountAmount(vouncher);

            if (discountAmount == -1)
            {
                return Content("Không tìm thấy voucher."); // Trả về thông báo nếu không tìm thấy voucher
            }

            return Content(""); // Trả về chuỗi rỗng nếu không có lỗi
        }

        public ActionResult FailureView()
        {
            return View();
        }

        public ActionResult SuccessView()
        {
            return View();
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/shoppingcart/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch
            {
                return View("FailureView");
            }
            try
            {
                Cart cart = Session["Cart"] as Cart;

                if (Session["UserCus"] == null)
                {
                    return RedirectToAction("Authen", "Login");
                }

                var cus = Session["ProfileCus"] as Customer;
                //Bảng hoá đơn sản phẩm

                if (cus == null)
                {
                    return Content("Lỗi xác thực");
                }
                else
                {
                    OrderPro order = new OrderPro();
                    order.DateOrder = DateTime.Now;
                    order.TypePayment = 2;
                    order.IDCus = cus.IDCus;
                    order.NameCusNonAccount = cus.NameCus;
                    order.PhoneCusNonAccount = cus.PhoneCus;
                    order.AddressDeliverry = cus.Diachi;

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
                            p.Quantity = updateQuantity; ;
                        }
                    }
                    database.SaveChanges();
                    cart.ClearCart();
                    //return RedirectToAction("CheckOut_Success", "ShoppingCart");
                }
            }
            catch
            {
                return Content("Lỗi thanh toán - Xin kiểm tra thông tin khách hàng...Xin cảm ơn.");
            }
            //on successful payment, show success page to user.  
            //return View("SuccessView");
            return RedirectToAction("SuccessView", "ShoppingCart");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listSanPham = Session["Cart"] as Cart;
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc
            foreach (var item in listSanPham.Items)
            {
                itemList.items.Add(new Item()
                {
                    name = item.product.NamePro,
                    currency = "USD",
                    price = item.product.Price.ToString(),
                    quantity = item.quantity.ToString(),
                    sku = item.product.ProductID.ToString(),
                });
            }
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = listSanPham.Total_money().ToString(),
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = listSanPham.Total_money().ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }
    }
}