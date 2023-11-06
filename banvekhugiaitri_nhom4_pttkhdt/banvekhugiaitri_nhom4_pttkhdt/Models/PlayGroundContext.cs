using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace banvekhugiaitri_nhom4_pttkhdt.Models;

public partial class PlayGroundContext : DbContext
{
    public PlayGroundContext()
    {
    }

    public PlayGroundContext(DbContextOptions<PlayGroundContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhKhuVuiChoi> AnhKhuVuiChois { get; set; }

    public virtual DbSet<Cthd> Cthds { get; set; }

    public virtual DbSet<CtveKhuVuiChoi> CtveKhuVuiChois { get; set; }

    public virtual DbSet<DaiLy> DaiLies { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<ThanhPho> ThanhPhos { get; set; }

    public virtual DbSet<VeKhuVuiChoi> VeKhuVuiChois { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-MIODTN1D;Initial Catalog=PlayGround;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhKhuVuiChoi>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AnhKhuVuiChoi");

            entity.Property(e => e.MaVe).ValueGeneratedOnAdd();
            entity.Property(e => e.TenFileAnh).HasMaxLength(200);
            entity.Property(e => e.ViTri).HasMaxLength(200);

            entity.HasOne(d => d.MaVeNavigation).WithMany()
                .HasForeignKey(d => d.MaVe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AnhKhuVuiChoi_VeKhuVuiChoi");
        });

        modelBuilder.Entity<Cthd>(entity =>
        {
            entity.HasKey(e => e.MaCthd).HasName("PK__CTHD__1E4FA771D9F52048");

            entity.ToTable("CTHD");

            entity.Property(e => e.MaCthd).HasColumnName("MaCTHD");
            entity.Property(e => e.Gia).HasColumnType("money");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.Cthds)
                .HasForeignKey(d => d.MaHd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CTHD_HoaDon");
        });

        modelBuilder.Entity<CtveKhuVuiChoi>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CTVeKhuVuiChoi");

            entity.Property(e => e.DiaDiem).HasMaxLength(100);
            entity.Property(e => e.MaCtve)
                .ValueGeneratedOnAdd()
                .HasColumnName("MaCTVe");
            entity.Property(e => e.UuDai).HasMaxLength(500);

            entity.HasOne(d => d.MaVeNavigation).WithMany()
                .HasForeignKey(d => d.MaVe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CTVeKhuVuiChoi_VeKhuVuiChoi");
        });

        modelBuilder.Entity<DaiLy>(entity =>
        {
            entity.HasKey(e => e.MaDaiLy).HasName("PK__DaiLy__069B00B395A82CFE");

            entity.ToTable("DaiLy");

            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.TenDaiLy).HasMaxLength(300);
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PK__HoaDon__2725A6E08E15B070");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.TongTien).HasColumnType("money");

            entity.HasOne(d => d.MaDaiLyNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaDaiLy)
                .HasConstraintName("fk_HoaDon_DaiLy");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("fk_HoaDon_KhachHang");

            entity.HasOne(d => d.MaVeNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaVe)
                .HasConstraintName("fk_HoaDon_VeKhuVuiChoi");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1E092E85F6");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.AnhKh)
                .HasMaxLength(100)
                .HasColumnName("AnhKH");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.SoCmnd)
                .HasMaxLength(20)
                .HasColumnName("SoCMND");
            entity.Property(e => e.TenKh)
                .HasMaxLength(30)
                .HasColumnName("TenKH");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TaiKhoan__3213E83F9FC52342");

            entity.ToTable("TaiKhoan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(30)
                .HasColumnName("matkhau");
            entity.Property(e => e.Taikhoan1)
                .HasMaxLength(30)
                .HasColumnName("taikhoan");

            entity.HasOne(d => d.MaDaiLyNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaDaiLy)
                .HasConstraintName("fk_TaiKhoan_DaiLy");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.TaiKhoans)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("fk_TaiKhoan_KhachHang");
        });

        modelBuilder.Entity<ThanhPho>(entity =>
        {
            entity.HasKey(e => e.MaTp).HasName("PK__ThanhPho__2725007D19853796");

            entity.ToTable("ThanhPho");

            entity.Property(e => e.MaTp).HasColumnName("MaTP");
            entity.Property(e => e.TenTp)
                .HasMaxLength(50)
                .HasColumnName("TenTP");
        });

        modelBuilder.Entity<VeKhuVuiChoi>(entity =>
        {
            entity.HasKey(e => e.MaVe).HasName("PK__VeKhuVui__2725100F73AAFD68");

            entity.ToTable("VeKhuVuiChoi");

            entity.Property(e => e.Active).HasDefaultValueSql("((0))");
            entity.Property(e => e.AnhKvc)
                .HasMaxLength(300)
                .HasColumnName("AnhKVC");
            entity.Property(e => e.Gia).HasColumnType("money");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isDeleted");
            entity.Property(e => e.MaTp).HasColumnName("MaTP");
            entity.Property(e => e.Slvcl).HasColumnName("SLVCL");
            entity.Property(e => e.TenVe).HasMaxLength(300);

            entity.HasOne(d => d.MaDaiLyNavigation).WithMany(p => p.VeKhuVuiChois)
                .HasForeignKey(d => d.MaDaiLy)
                .HasConstraintName("fk_VeKhuVuiChoi_DaiLy");

            entity.HasOne(d => d.MaTpNavigation).WithMany(p => p.VeKhuVuiChois)
                .HasForeignKey(d => d.MaTp)
                .HasConstraintName("fk_VeKhuVuiChoi_ThanhPho");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
