using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CuaHangSach.Models
{
    public partial class CuaHangSachModel : DbContext
    {
        public CuaHangSachModel()
            : base("name=CuaHangSachDbContext")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<tbl_KhachHang> tbl_KhachHang { get; set; }
        public virtual DbSet<tbl_Sach> tbl_Sach { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonDatHang>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.DonDatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_KhachHang>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_KhachHang>()
                .HasMany(e => e.DonDatHangs)
                .WithRequired(e => e.tbl_KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Sach>()
                .Property(e => e.AnhBia)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Sach>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.tbl_Sach)
                .WillCascadeOnDelete(false);
        }
    }
}
