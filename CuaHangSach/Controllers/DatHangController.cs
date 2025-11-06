using CuaHangSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangSach.Controllers
{
    public class DatHangController : Controller
    {
        CuaHangSachModel data = new CuaHangSachModel();

        private GioHang LayGioHang()
        {
            GioHang gh = Session["gh"] as GioHang;
            if (gh == null)
            {
                gh = new GioHang();
                Session["gh"] = gh;
            }
            return gh;
        }

        public ActionResult ThemMatHangMoi(int msp)
        {
            GioHang gh = LayGioHang();
            gh.Them(msp);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult XoaMatHang(int msp)
        {
            GioHang gh = LayGioHang();
            gh.Xoa(msp);
            return RedirectToAction("XemGioHang");
        }

        [HttpPost]
        public ActionResult CapNhatGioHang(int msp, int soluong)
        {
            GioHang gh = LayGioHang();
            gh.CapNhat(msp, soluong);
            return RedirectToAction("XemGioHang");
        }

        public ActionResult XemGioHang()
        {
            GioHang gh = LayGioHang();
            return View(gh);
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["kh"] == null)
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

            GioHang gh = LayGioHang();
            if (gh.SoMatHang() == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            tbl_KhachHang kh = Session["kh"] as tbl_KhachHang;
            ViewBag.KhachHang = kh;
            ViewBag.GioHang = gh;

            return View();
        }

        [HttpPost]
        public ActionResult TaoDonDatHang(FormCollection col)
        {
            tbl_KhachHang kh = Session["kh"] as tbl_KhachHang;
            GioHang gh = LayGioHang();

            if (kh == null || gh == null || gh.SoMatHang() == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            DateTime ngayGiao;
            if (!DateTime.TryParse(col["txtDate"], out ngayGiao))
            {
                ViewBag.KhachHang = kh;
                ViewBag.GioHang = gh;
                ViewBag.LoiNgay = "Ngày giao không hợp lệ.";
                return View("DatHang");
            }

            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            ddh.NgayGiao = ngayGiao;
            ddh.TinhTrangGiaoHang = 0;
            ddh.DaThanhToan = false;

            data.DonDatHangs.Add(ddh);
            data.SaveChanges();

            foreach (var item in gh.lst)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDon = ddh.MaDon;
                ctdh.MaSach = item.iMaSach;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dDonGia;

                data.ChiTietDonHangs.Add(ctdh);
            }

            data.SaveChanges();

            Session["gh"] = null;

            return RedirectToAction("XacNhanDonHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult GioHangPartial()
        {
            GioHang gh = LayGioHang();
            ViewBag.TongSoLuong = gh.TongSLHang();
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                data.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}