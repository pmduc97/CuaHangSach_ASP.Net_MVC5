using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuaHangSach.Models
{
    public class Function
    {
        CuaHangSachEntities db = new CuaHangSachEntities();
        public int sumSachTheoLoai(string maloai)
        {
            return db.SACHes.Where(p => p.MaLoai.Equals(maloai)).Count();
        }

        public long tongTien(string mahoadon)
        {
            var t = db.CHITIETHOADONs.Where(p => p.MaHoaDon.Equals(mahoadon));
            if(t.Count() == 0)
            {
                return 0;
            }
            else
            {
                return t.Sum(p => p.ThanhTien);
            }
        }
    }
}