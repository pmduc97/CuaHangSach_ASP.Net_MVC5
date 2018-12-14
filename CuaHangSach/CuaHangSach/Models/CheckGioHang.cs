using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CuaHangSach.Models;

namespace CuaHangSach.Models
{
    public class CheckGioHang
    {
        public int checkTrung(string masach, List<SACH> lstSach)
        {
            var t = lstSach.Where(p => p.MaSach.Equals(masach));
            if (t.Count() != 0)
            {
                SACH s = t.First();
                s.SoLuong = s.SoLuong + 1;
                return 1;
            }
            else
            {
                return -1;
            }
        }

    }
}