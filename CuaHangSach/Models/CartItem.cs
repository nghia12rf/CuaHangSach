using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CuaHangSach.Models;

namespace CuaHangSach.Models
{
    public class CartItem
    {
        // Sử dụng DbContext đã tạo ở Bước 1
        CuaHangSachModel data = new CuaHangSachModel();

        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        // Hàm tạo (Constructor) để lấy thông tin sách từ CSDL
        public CartItem(int MaSach)
        {
            iMaSach = MaSach;

            // THAY ĐỔI: Dùng Find() của EF thay vì Single() của LINQ
            tbl_Sach sach = data.tbl_Sach.Find(iMaSach);

            sTenSach = sach.TenSach;
            sAnhBia = sach.AnhBia;
            dDonGia = double.Parse(sach.DonGia.ToString());
            iSoLuong = 1; // Số lượng mặc định khi thêm vào là 1
        }
    }
}