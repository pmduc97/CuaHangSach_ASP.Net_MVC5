using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangSach.Models;

namespace CuaHangSach.Controllers
{
    public class KhachHangController : Controller
    {
        private CuaHangSachEntities db = new CuaHangSachEntities();
        // GET: KhachHang
        public ActionResult Index()
        {
            return View();
        }

        // GET: KhachHang/DangNhap
        public ActionResult DangNhap()
        {
            return View();
        }

        // POST: KhachHang/DangNhap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string taikhoan,string matkhau)
        {
            var t = db.KHACHHANGs.Where(p => p.MaKhachHang.Equals(taikhoan) && p.MatKhau.Equals(matkhau));
            if(t.Count() == 0)
            {
                ViewBag.KiemTra = "0";
            }
            else
            {
                ViewBag.KiemTra = "1";
                Session["KhachHang"] = t.First();

                return RedirectToAction("Index", "TrangChu");
            }
            return View();
        }

        // GET: KhachHang/DangXuat
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "TrangChu");
        }

        // GET: KhachHang/DangKy
        public ActionResult DangKy()
        {
            return View();
        }
    }
}