using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangSach.Models;

namespace CuaHangSach.Controllers
{
    public class DonHangCuaBanController : Controller
    {
        private CuaHangSachEntities db = new CuaHangSachEntities();

        // GET: DonHangCuaBan
        public ActionResult Index()
        {
            KHACHHANG kh = (KHACHHANG)Session["KhachHang"];
            var t = db.HOADONs.Where(p => p.MaKhachHang.Equals(kh.MaKhachHang));
            return View(t.ToList());
        }

        public ActionResult ChiTiet(string mahoadon)
        {
            if(mahoadon == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                ViewBag.MaHoaDon = mahoadon;
                var t = db.View_HienThiHoaDon.Where(p => p.MaHoaDon.Equals(mahoadon));
                return View(t.ToList());
            }
        }
    }
}