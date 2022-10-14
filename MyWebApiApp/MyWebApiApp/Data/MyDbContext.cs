using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options): base(options) { }

        #region DbSet
        public DbSet<HangHoa> hangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> donHangs { get; set; }
        public DbSet<DonHangChiTiet> donHangChiTiets { get; set; }
        #endregion 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.MaDh);
                e.Property(dh => dh.NgayDat).HasDefaultValueSql("getutcdate()");
            });

            modelBuilder.Entity<DonHangChiTiet>(entity =>
            {
                entity.ToTable("ChiTietDonHang");
                entity.HasKey(e => new { e.Mahh, e.MaDh });

                entity.HasOne(e => e.donHang).WithMany(e => e.donHangChiTiets).HasForeignKey(e => e.MaDh).HasConstraintName("FK_DonHangCT_DonHang");
                entity.HasOne(e => e.hangHoa).WithMany(e => e.donHangChiTiets).HasForeignKey(e => e.Mahh).HasConstraintName("FK_DonHangCT_HangHoa");
            });
        }

    }
}
