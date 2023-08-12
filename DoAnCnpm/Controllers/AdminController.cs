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
        DoAnCNPMEntities database = new DoAnCNPMEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(AdminUser _user)
        {
            var check = database.AdminUsers.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo="Sai Info";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["ID"]= _user.ID;
                Session["NameUser"]= _user.NameUser;
                Session["RoleUser"]= _user.RoleUser;
                return RedirectToAction("TestAD","Home");
            }
        }
        public ActionResult RegisterAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterAdmin(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = database.AdminUsers.Where(s => s.ID == _user.ID).FirstOrDefault();
                if (check_ID == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.AdminUsers.Add(_user);
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
        public ActionResult LogOutAdmin()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginUser");
        }
    }
}
