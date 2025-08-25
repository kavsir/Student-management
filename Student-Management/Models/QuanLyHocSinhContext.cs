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
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QuanLyHocSinh;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => e.MaDiem).HasName("PK__DIEM__33326025B024CD28");

            entity.ToTable("DIEM");

            entity.Property(e => e.DiemTb).HasColumnName("DiemTB");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.MaHs).HasColumnName("MaHS");

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__MaHK__44FF419A");

            entity.HasOne(d => d.MaHsNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__MaHS__4316F928");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__MaMonHoc__440B1D61");
        });

        modelBuilder.Entity<Giaovien>(entity =>
        {
            entity.HasKey(e => e.MaGv).HasName("PK__GIAOVIEN__2725AEF3F08E4A54");

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
                .HasConstraintName("FK__GIAOVIEN__MaMonH__2B3F6F97");
        });

        modelBuilder.Entity<Hocky>(entity =>
        {
            entity.HasKey(e => e.MaHk).HasName("PK__HOCKY__2725A6E70D91B9C2");

            entity.ToTable("HOCKY");

            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.TenHk)
                .HasMaxLength(50)
                .HasColumnName("TenHK");

            entity.HasOne(d => d.MaNamHocNavigation).WithMany(p => p.Hockies)
                .HasForeignKey(d => d.MaNamHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOCKY__MaNamHoc__267ABA7A");
        });

        modelBuilder.Entity<Hocphi>(entity =>
        {
            entity.HasKey(e => e.MaHp).HasName("PK__HOCPHI__2725A6EC56096092");

            entity.ToTable("HOCPHI");

            entity.Property(e => e.MaHp).HasColumnName("MaHP");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.MaHs).HasColumnName("MaHS");
            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.Hocphis)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOCPHI__MaHK__48CFD27E");

            entity.HasOne(d => d.MaHsNavigation).WithMany(p => p.Hocphis)
                .HasForeignKey(d => d.MaHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOCPHI__MaHS__47DBAE45");
        });

        modelBuilder.Entity<Hocsinh>(entity =>
        {
            entity.HasKey(e => e.MaHs).HasName("PK__HOCSINH__2725A6EFAF72A5EA");

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
                .HasConstraintName("FK__HOCSINH__MaLop__31EC6D26");
        });

        modelBuilder.Entity<Lichhoc>(entity =>
        {
            entity.HasKey(e => e.MaLichHoc).HasName("PK__LICHHOC__150EBC219A604421");

            entity.ToTable("LICHHOC");

            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.ThuTrongTuan).HasMaxLength(20);

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaGv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaGV__3E52440B");

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaHK__403A8C7D");

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaLop__3C69FB99");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaMonHo__3D5E1FD2");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaPhong__3F466844");
        });

        modelBuilder.Entity<Lop>(entity =>
        {
            entity.HasKey(e => e.MaLop).HasName("PK__LOP__3B98D273D3FFE161");

            entity.ToTable("LOP");

            entity.Property(e => e.MaGvcn).HasColumnName("MaGVCN");
            entity.Property(e => e.TenLop).HasMaxLength(50);

            entity.HasOne(d => d.MaGvcnNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.MaGvcn)
                .HasConstraintName("FK__LOP__MaGVCN__2F10007B");

            entity.HasOne(d => d.MaNamHocNavigation).WithMany(p => p.Lops)
                .HasForeignKey(d => d.MaNamHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LOP__MaNamHoc__2E1BDC42");
        });

        modelBuilder.Entity<Monhoc>(entity =>
        {
            entity.HasKey(e => e.MaMonHoc).HasName("PK__MONHOC__4127737FFC734FAB");

            entity.ToTable("MONHOC");

            entity.Property(e => e.TenMonHoc).HasMaxLength(100);
        });

        modelBuilder.Entity<Namhoc>(entity =>
        {
            entity.HasKey(e => e.MaNamHoc).HasName("PK__NAMHOC__7DBADD7402B5CB7D");

            entity.ToTable("NAMHOC");

            entity.Property(e => e.TenNamHoc).HasMaxLength(50);
        });

        modelBuilder.Entity<PhancongGiangday>(entity =>
        {
            entity.HasKey(e => e.MaPc).HasName("PK__PHANCONG__2725E7E5C12A8569");

            entity.ToTable("PHANCONG_GIANGDAY");

            entity.Property(e => e.MaPc).HasColumnName("MaPC");
            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaGv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG_G__MaGV__36B12243");

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG_G__MaHK__398D8EEE");

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG___MaLop__38996AB5");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG___MaMon__37A5467C");
        });

        modelBuilder.Entity<Phonghoc>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__PHONGHOC__20BD5E5BAB8E7D58");

            entity.ToTable("PHONGHOC");

            entity.Property(e => e.TenPhong).HasMaxLength(50);
            entity.Property(e => e.ViTri).HasMaxLength(100);
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TAIKHOAN__27250070FB09D618");

            entity.ToTable("TAIKHOAN");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TAIKHOAN__55F68FC0DBA1FD3A").IsUnique();

            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.MaHs).HasColumnName("MaHS");
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.VaiTro).HasMaxLength(20);

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.MaGv)
                .HasConstraintName("FK__TAIKHOAN__MaGV__4D94879B");

            entity.HasOne(d => d.MaHsNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.MaHs)
                .HasConstraintName("FK__TAIKHOAN__MaHS__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
