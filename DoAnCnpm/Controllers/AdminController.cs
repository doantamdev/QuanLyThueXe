using DoAnCnpm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCnpm.Controllers
{
    public class AdminController : Controller
    {
        // GET: LoginUser
        DoAnPMEntities database = new DoAnPMEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(Admin _user)
        {
            var check = database.Admins.Where(s => s.Username == _user.Username && s.Password == _user.Password).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo="Sai Info";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"]= _user.Id;
                Session["Username"]= _user.Username;
                return RedirectToAction("TestAD","Home");
            }
        }
        public ActionResult RegisterAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterAdmin(Admin _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = database.Admins.Where(s => s.Id == _user.Id).FirstOrDefault();
                if (check_ID == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.Admins.Add(_user);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister= "This ID is exixst";
                    return View();
                }
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Admin");
        }
    }
}
