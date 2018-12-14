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
        public ActionResult DangNhap(string next)
        {
            ViewBag.Next = next??"TrangChu";
            return View();
        }

        // POST: KhachHang/DangNhap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string taikhoan,string matkhau,string next)
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

                return RedirectToAction("Index", next);
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

        // POST: KhachHang/DangKy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(string taikhoan,string matkhau,string hoten,string email,string quequan,string dienthoai,string ngaysinh,string gioitinh)
        {
            var t = db.KHACHHANGs.Where(p => p.MaKhachHang.Equals(taikhoan));
            if(t.Count() != 0)
            {
                ViewBag.KiemTra = "-1";
                return View();
            }
            else
            {
                KHACHHANG kh = new KHACHHANG();
                kh.Email = email;
                kh.MaKhachHang = taikhoan;
                kh.MatKhau = matkhau;
                kh.QueQuan = quequan;
                kh.SoDienThoai = dienthoai;
                kh.GioiTinh = gioitinh.Equals("0") ? true : false;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                kh.TenKhachHang = hoten;

                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                ViewBag.KiemTra = "1";
                return View();
            }
        }
    }
}