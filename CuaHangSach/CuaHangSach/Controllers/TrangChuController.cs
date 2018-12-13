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
                lstSach = lstSach.Where(p => p.TenSach.Contains(key)).OrderBy(x => x.TenSach).ToList();
            }
            if (page == null) page = 1;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(lstSach.ToPagedList(pageNumber,pageSize));
        }
    }
}