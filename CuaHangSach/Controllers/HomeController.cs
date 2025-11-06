using CuaHangSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangSach.Controllers
{
    public class HomeController : Controller
    {
        CuaHangSachModel data = new CuaHangSachModel();

        // GET: Home
        public ActionResult Index()
        {
            
            var dsSach = data.tbl_Sach.ToList();
            return View(dsSach);
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