using DoAnCnpm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCnpm.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        //[Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View(database.Roles.ToList());
        }
        DoAnCNPMEntities database = new DoAnCNPMEntities();

        // GET: Categories
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Role role)
        {
            try
            {
                database.Roles.Add(role);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi tạo mới");
            }
        }
        public ActionResult Details(int id)
        {
            return View(database.Roles.Where(s => s.IdRole == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(database.Roles.Where(s => s.IdRole == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Role role)
        {
            database.Entry(role).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("index");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Roles.Where(s => s.IdRole == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Role role)
        {
            try
            {
                role = database.Roles.Where(s => s.IdRole == id).FirstOrDefault();
                database.Roles.Remove(role);
                database.SaveChanges();
                return RedirectToAction("index");
            }
            catch
            {
                return Content("Không xóa được");
            }
        }
    }
}