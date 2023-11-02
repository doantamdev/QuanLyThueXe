using DoAnCnpm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCnpm.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DoAnPMEntities database = new DoAnPMEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authen()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authen(Customer customer)
        {
            var check = database.Customers.Where(s => s.UserCus.Equals(customer.UserCus) &&
            s.PassCus.Equals(customer.PassCus)).FirstOrDefault();
            if (check == null)
            {
                customer.LoginErrorMessage = "Sai tài khoản hoặc mật khẩu, vui lòng thử lại!";
                return View("Authen", customer);
            }
            else
            {
                Session["UserCus"] = customer.UserCus;
                Session["IDCus"] = customer.IDCus;
                Session["UserId"] = check.IDCus;
                Session["ProfileCus"] = check;
                if (check.Gioitinh == 0)
                {
                    return RedirectToAction("Sukienphunu", "Login");
                }
                return RedirectToAction("Index", "Product");
            }
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var check = database.Customers.FirstOrDefault(s => s.UserCus == customer.UserCus);
                if (check == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.Customers.Add(customer);
                    database.SaveChanges();                
                    return RedirectToAction("Authen", "Login");
                }
                else
                {
                    ViewBag.error = "Tài khoản đã tồn tại thử lại tới tên tài khoản khác";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Sukienphunu()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Authen", "Login");
        }

        public ActionResult QuanLyKhachHang()
        {
            return View(database.Customers.ToList());
        }
        public ActionResult Delete(int id)
        {
            return View(database.Customers.Where(s => s.IDCus == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {
            try
            {
                customer = database.Customers.Where(s => s.IDCus == id).FirstOrDefault();
                database.Customers.Remove(customer);
                database.SaveChanges();
                return RedirectToAction("QuanLyKhachHang");
            }
            catch
            {
                return Content("Không xóa được");
            }
        }
    }
}