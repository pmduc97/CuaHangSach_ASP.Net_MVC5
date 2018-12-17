using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangSach.Models;
using PagedList;

namespace CuaHangSach.Controllers
{
    public class TrangChuController : Controller
    {
        private CuaHangSachEntities db = new CuaHangSachEntities();
        private CheckGioHang gh = new CheckGioHang();
        // GET: TrangChu
        public ActionResult Index(string maloai,string key,int? page)
        {
            ViewBag.TieuDe = "Trang Chủ";
            var t = db.LOAIs;
            ViewBag.lstLoai = t;

            List<SACH> lstSach = db.SACHes.OrderBy(p => p.TenSach).ToList();

            if(maloai != null && key == null)
            {
                lstSach = lstSach.Where(p => p.MaLoai.Equals(maloai)).OrderBy(x=>x.TenSach).ToList();
            }
            else if(key != null && maloai == null)
            {
                lstSach = lstSach.Where(p => p.TenSach.ToLower().Contains(key.ToLower())).OrderBy(x => x.TenSach).ToList();
            }
            if (page == null) page = 1;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(lstSach.ToPagedList(pageNumber,pageSize));
        }

        // POST: TrangChu/ThemGio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemGio([Bind(Include = "MaSach,TenSach,SoLuong,Gia,MaLoai,Anh,TacGia,NgayNhap")] SACH sACH)
        {
                if (Session["GioHang"] == null)
                {
                    Session["GioHang"] = new List<SACH>();
                }
                    List<SACH> lstSach = (List<SACH>)Session["GioHang"];
            int i = gh.checkTrung(sACH.MaSach, lstSach);
            if(i == -1)
            {
                lstSach.Add(sACH);
            }  
                    Session.Add("GioHang", lstSach);
                return RedirectToAction("Index","GioHang");
        }
    }
}