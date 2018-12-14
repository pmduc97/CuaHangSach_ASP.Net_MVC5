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

        // GET: GioHang/DatHang
        public ActionResult DatHang()
        {
            if(Session["GioHang"] != null && Session["KhachHang"] != null)
            {
                KHACHHANG kh = (KHACHHANG)Session["KhachHang"];
                List<SACH> lstGioHang = (List<SACH>)Session["GioHang"];

                HOADON hd = new HOADON();
                hd.DaThanhToan = false;
                hd.MaKhachHang = kh.MaKhachHang;
                hd.ThoiGianMua = DateTime.Now;
                hd.MaHoaDon = kh.MaKhachHang + "_" + hd.ThoiGianMua.ToString("ddMMyyyy_HHmmss");
                string mahoadon = hd.MaHoaDon;
                db.HOADONs.Add(hd);
                db.SaveChanges();

                long macthd = 1;
                var t = db.CHITIETHOADONs;
                if(t.Count() != 0)
                {
                    macthd = t.Max(p => p.MaChiTietHoaDon) + 1;
                }
                foreach(SACH s in lstGioHang)
                {
                    CHITIETHOADON cthd = new CHITIETHOADON();
                    cthd.MaHoaDon = mahoadon;
                    cthd.MaSach = s.MaSach;
                    cthd.SoLuongMua = int.Parse(s.SoLuong.ToString());
                    cthd.ThanhTien = cthd.SoLuongMua * s.Gia;
                    cthd.MaChiTietHoaDon = macthd;
                    db.CHITIETHOADONs.Add(cthd);
                    db.SaveChanges();
                    macthd++;
                }

                Session["GioHang"] = new List<SACH>();
                return RedirectToAction("ChiTiet", "DonHangCuaBan", new { mahoadon = mahoadon});
            }
            else
            {
                return RedirectToAction("Index", "TrangChu");
            }
        }
    }
}