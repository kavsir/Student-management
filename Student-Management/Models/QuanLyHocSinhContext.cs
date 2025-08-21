using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Student_Management.Models;

public partial class QuanLyHocSinhContext : DbContext
{
    public QuanLyHocSinhContext()
    {
    }

    public QuanLyHocSinhContext(DbContextOptions<QuanLyHocSinhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Diem> Diems { get; set; }

    public virtual DbSet<Giaovien> Giaoviens { get; set; }

    public virtual DbSet<Hocky> Hockies { get; set; }

    public virtual DbSet<Hocphi> Hocphis { get; set; }

    public virtual DbSet<Hocsinh> Hocsinhs { get; set; }

    public virtual DbSet<Lichhoc> Lichhocs { get; set; }

    public virtual DbSet<Lop> Lops { get; set; }

    public virtual DbSet<Monhoc> Monhocs { get; set; }

    public virtual DbSet<Namhoc> Namhocs { get; set; }

    public virtual DbSet<PhancongGiangday> PhancongGiangdays { get; set; }

    public virtual DbSet<Phonghoc> Phonghocs { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-3FRC4HS\\SQL;Database=QuanLyHocSinh;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => e.MaDiem).HasName("PK__DIEM__33326025B22EF476");

            entity.ToTable("DIEM");

            entity.Property(e => e.DiemTb).HasColumnName("DiemTB");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.MaHs).HasColumnName("MaHS");

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__MaHK__300424B4");

            entity.HasOne(d => d.MaHsNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__MaHS__2E1BDC42");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__MaMonHoc__2F10007B");
        });

        modelBuilder.Entity<Giaovien>(entity =>
        {
            entity.HasKey(e => e.MaGv).HasName("PK__GIAOVIEN__2725AEF39389227A");

            entity.ToTable("GIAOVIEN");

            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.Giaoviens)
                .HasForeignKey(d => d.MaMonHoc)
                .HasConstraintName("FK__GIAOVIEN__MaMonH__1920BF5C");
        });

        modelBuilder.Entity<Hocky>(entity =>
        {
            entity.HasKey(e => e.MaHk).HasName("PK__HOCKY__2725A6E788B332F2");

            entity.ToTable("HOCKY");

            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.TenHk)
                .HasMaxLength(50)
                .HasColumnName("TenHK");

            entity.HasOne(d => d.MaNamHocNavigation).WithMany(p => p.Hockies)
                .HasForeignKey(d => d.MaNamHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOCKY__MaNamHoc__1273C1CD");
        });

        modelBuilder.Entity<Hocphi>(entity =>
        {
            entity.HasKey(e => e.MaHp).HasName("PK__HOCPHI__2725A6EC0C6432F3");

            entity.ToTable("HOCPHI");

            entity.Property(e => e.MaHp).HasColumnName("MaHP");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.MaHs).HasColumnName("MaHS");
            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.Hocphis)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOCPHI__MaHK__33D4B598");

            entity.HasOne(d => d.MaHsNavigation).WithMany(p => p.Hocphis)
                .HasForeignKey(d => d.MaHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOCPHI__MaHS__32E0915F");
        });

        modelBuilder.Entity<Hocsinh>(entity =>
        {
            entity.HasKey(e => e.MaHs).HasName("PK__HOCSINH__2725A6EF469D382F");

            entity.ToTable("HOCSINH");

            entity.Property(e => e.MaHs).HasColumnName("MaHS");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.TrangThai).HasMaxLength(20);

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.Hocsinhs)
                .HasForeignKey(d => d.MaLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOCSINH__MaLop__1CF15040");
        });

        modelBuilder.Entity<Lichhoc>(entity =>
        {
            entity.HasKey(e => e.MaLichHoc).HasName("PK__LICHHOC__150EBC21ED70A932");

            entity.ToTable("LICHHOC");

            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.ThuTrongTuan).HasMaxLength(20);

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaGv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaGV__29572725");

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaHK__2B3F6F97");

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaLop__276EDEB3");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaMonHo__286302EC");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaPhong__2A4B4B5E");
        });

        modelBuilder.Entity<Lop>(entity =>
        {
            entity.HasKey(e => e.MaLop).HasName("PK__LOP__3B98D273B68CF296");

            entity.ToTable("LOP");

            entity.Property(e => e.MaGvcn).HasColumnName("MaGVCN");
            entity.Property(e => e.TenLop).HasMaxLength(50);

            entity.HasOne(d => d.MaGvcnNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.MaGvcn)
                .HasConstraintName("FK_LOP_GIAOVIEN");

            entity.HasOne(d => d.MaNamHocNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.MaNamHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOP_NAMHOC");
        });

        modelBuilder.Entity<Monhoc>(entity =>
        {
            entity.HasKey(e => e.MaMonHoc).HasName("PK__MONHOC__4127737FEFB51892");

            entity.ToTable("MONHOC");

            entity.Property(e => e.TenMonHoc).HasMaxLength(100);
        });

        modelBuilder.Entity<Namhoc>(entity =>
        {
            entity.HasKey(e => e.MaNamHoc).HasName("PK__NAMHOC__7DBADD74EB59B4B7");

            entity.ToTable("NAMHOC");

            entity.Property(e => e.TenNamHoc).HasMaxLength(50);
        });

        modelBuilder.Entity<PhancongGiangday>(entity =>
        {
            entity.HasKey(e => e.MaPc).HasName("PK__PHANCONG__2725E7E529B42D7B");

            entity.ToTable("PHANCONG_GIANGDAY");

            entity.Property(e => e.MaPc).HasColumnName("MaPC");
            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaGv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG_G__MaGV__21B6055D");

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG_G__MaHK__24927208");

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG___MaLop__239E4DCF");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG___MaMon__22AA2996");
        });

        modelBuilder.Entity<Phonghoc>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__PHONGHOC__20BD5E5B5B2BEA5C");

            entity.ToTable("PHONGHOC");

            entity.Property(e => e.TenPhong).HasMaxLength(50);
            entity.Property(e => e.ViTri).HasMaxLength(100);
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TAIKHOAN__27250070D339FE88");

            entity.ToTable("TAIKHOAN");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TAIKHOAN__55F68FC04608D53A").IsUnique();

            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.MaHs).HasColumnName("MaHS");
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.VaiTro).HasMaxLength(20);

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.MaGv)
                .HasConstraintName("FK__TAIKHOAN__MaGV__38996AB5");

            entity.HasOne(d => d.MaHsNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.MaHs)
                .HasConstraintName("FK__TAIKHOAN__MaHS__37A5467C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
