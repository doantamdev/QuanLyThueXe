using DoAnCnpm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnCnpm.Controllers
{
    public class VoucherController : Controller
    {
        DoAnPMEntities database = new DoAnPMEntities();

        // Action để hiển thị danh sách voucher
        public ActionResult Index()
        {
            List<Voucher> vouchers = database.Vouchers.ToList();
            return View(vouchers);
        }

        // Action để tạo voucher mới (GET)
        public ActionResult Create()
        {
            return View();
        }

        // Action để tạo voucher mới (POST)
        [HttpPost]
        public ActionResult Create(Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                database.Vouchers.Add(voucher);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voucher);
        }

        // Action để hiển thị trang sửa voucher (GET)
        public ActionResult Edit(int id)
        {
            Voucher voucher = database.Vouchers.Find(id);
            if (voucher == null)
            {
                return RedirectToAction("Index");
            }
            return View(voucher);
        }

        // Action để sửa voucher (POST)
        [HttpPost]
        public ActionResult Edit(Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                database.Entry(voucher).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voucher);
        }

        // Action để xóa voucher
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Voucher voucher = database.Vouchers.Find(id);
            if (voucher != null)
            {
                database.Vouchers.Remove(voucher);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                database.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}