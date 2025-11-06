using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CuaHangSach.Models;

namespace CuaHangSach.Models
{
    public class GioHang
    {
        public List<CartItem> lst;

        public GioHang()
        {
            lst = new List<CartItem>();
        }

        public GioHang(List<CartItem> lstGH)
        {
            lst = lstGH;
        }

        public int SoMatHang()
        {
            if (lst == null)
                return 0;
            return lst.Count;
        }

        public int TongSLHang()
        {
            if (lst == null)
                return 0;
            return lst.Sum(n => n.iSoLuong);
        }

        public double TongThanhTien()
        {
            if (lst == null)
                return 0;
            return lst.Sum(n => n.dThanhTien);
        }

        public void Them(int iMaSach)
        {
            CartItem sanpham = lst.Find(n => n.iMaSach == iMaSach);

            if (sanpham == null)
            {
                CartItem sach = new CartItem(iMaSach);
                if (sach != null)
                {
                    lst.Add(sach);
                }
            }
            else
            {
                sanpham.iSoLuong++;
            }
        }

        public void Xoa(int iMaSach)
        {
            CartItem sanpham = lst.Find(n => n.iMaSach == iMaSach);
            if (sanpham != null)
            {
                lst.Remove(sanpham);
            }
        }

        public void CapNhat(int iMaSach, int iSoLuongMoi)
        {
            CartItem sanpham = lst.Find(n => n.iMaSach == iMaSach);
            if (sanpham != null)
            {
                sanpham.iSoLuong = iSoLuongMoi;
            }
        }
    }
}