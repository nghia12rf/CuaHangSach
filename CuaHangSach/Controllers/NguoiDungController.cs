using CuaHangSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangSach.Controllers
{
    public class NguoiDungController : Controller
    {
        CuaHangSachModel data = new CuaHangSachModel();

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection col)
        {
            string tenDN = col["txtTenName"];
            string matKhau = col["txtPass"];

            tbl_KhachHang kh = data.tbl_KhachHang.FirstOrDefault(
                k => k.TenKH == tenDN && k.MatKhau == matKhau);

            if (kh != null)
            {
                Session["kh"] = kh;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }
        }

        public ActionResult DangXuat()
        {
            Session["kh"] = null;
            Session["gh"] = null;
            return RedirectToAction("Index", "Home");
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