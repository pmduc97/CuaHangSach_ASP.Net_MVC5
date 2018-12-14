using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangSach.Models;

namespace CuaHangSach.Controllers
{
    public class GioHangController : Controller
    {
        private CuaHangSachEntities db = new CuaHangSachEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        // GET: GioHang/CapNhat
        public ActionResult CapNhat(int? soluong,string masach)
        {
            List<SACH> lstGioHang = (List<SACH>)Session["GioHang"];
            var t = lstGioHang.Where(p => p.MaSach.Equals(masach)).First();
            t.SoLuong = soluong??t.SoLuong;
            Session["GioHang"] = lstGioHang;
            return RedirectToAction("Index");
        }

        // POST: GioHang/Xoa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Xoa(string masach)
        {
            List<SACH> lstGioHang = (List<SACH>)Session["GioHang"];
            var t = lstGioHang.Where(p => p.MaSach.Equals(masach)).First();
            lstGioHang.Remove(t);
            return RedirectToAction("Index");
        }
    }
}