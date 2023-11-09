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
        DoAnPMEntities database = new DoAnPMEntities();

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
            return View(database.Roles.Where(s => s.Id == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(database.Roles.Where(s => s.Id == id).FirstOrDefault());
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
            return View(database.Roles.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Role role)
        {
            try
            {
                role = database.Roles.Where(s => s.Id == id).FirstOrDefault();
                database.Roles.Remove(role);
                database.SaveChanges();
                return RedirectToAction("index");
            }
            catch
            {
                return Content("Không xóa được");
            }
        }
        public ActionResult Index(string _name)
        {
            if (_name == null)
            {
                return View(database.Roles.ToList());
            }
            else
            {
                return View(database.Roles.Where(s => s.RoleName.Contains(_name)).ToList());
            }
        }

    }
}