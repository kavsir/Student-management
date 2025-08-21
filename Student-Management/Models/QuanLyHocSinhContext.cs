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
        => optionsBuilder.UseSqlServer("Server=SEELE;Database=QuanLyHocSinh;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diem>(entity =>
        {
            entity.HasKey(e => e.MaDiem).HasName("PK__DIEM__333260251E11A3DB");

            entity.ToTable("DIEM");

            entity.Property(e => e.DiemTb).HasColumnName("DiemTB");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.MaHs).HasColumnName("MaHS");

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__MaHK__571DF1D5");

            entity.HasOne(d => d.MaHsNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__MaHS__5535A963");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.Diems)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIEM__MaMonHoc__5629CD9C");
        });

        modelBuilder.Entity<Giaovien>(entity =>
        {
            entity.HasKey(e => e.MaGv).HasName("PK__GIAOVIEN__2725AEF346489330");

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
                .HasConstraintName("FK__GIAOVIEN__MaMonH__403A8C7D");
        });

        modelBuilder.Entity<Hocky>(entity =>
        {
            entity.HasKey(e => e.MaHk).HasName("PK__HOCKY__2725A6E70B459F35");

            entity.ToTable("HOCKY");

            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.TenHk)
                .HasMaxLength(50)
                .HasColumnName("TenHK");

            entity.HasOne(d => d.MaNamHocNavigation).WithMany(p => p.Hockies)
                .HasForeignKey(d => d.MaNamHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOCKY__MaNamHoc__398D8EEE");
        });

        modelBuilder.Entity<Hocphi>(entity =>
        {
            entity.HasKey(e => e.MaHp).HasName("PK__HOCPHI__2725A6EC2F368F8F");

            entity.ToTable("HOCPHI");

            entity.Property(e => e.MaHp).HasColumnName("MaHP");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.MaHs).HasColumnName("MaHS");
            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai).HasMaxLength(50);

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.Hocphis)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOCPHI__MaHK__5AEE82B9");

            entity.HasOne(d => d.MaHsNavigation).WithMany(p => p.Hocphis)
                .HasForeignKey(d => d.MaHs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOCPHI__MaHS__59FA5E80");
        });

        modelBuilder.Entity<Hocsinh>(entity =>
        {
            entity.HasKey(e => e.MaHs).HasName("PK__HOCSINH__2725A6EF7FBD72DA");

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
                .HasConstraintName("FK__HOCSINH__MaLop__440B1D61");
        });

        modelBuilder.Entity<Lichhoc>(entity =>
        {
            entity.HasKey(e => e.MaLichHoc).HasName("PK__LICHHOC__150EBC21BB63C0B0");

            entity.ToTable("LICHHOC");

            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");
            entity.Property(e => e.ThuTrongTuan).HasMaxLength(20);

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaGv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaGV__5070F446");

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaHK__52593CB8");

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaLop__4E88ABD4");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaMonHo__4F7CD00D");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.Lichhocs)
                .HasForeignKey(d => d.MaPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LICHHOC__MaPhong__5165187F");
        });

        modelBuilder.Entity<Lop>(entity =>
        {
            entity.HasKey(e => e.MaLop).HasName("PK__LOP__3B98D2733854AD5E");

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
            entity.HasKey(e => e.MaMonHoc).HasName("PK__MONHOC__4127737F68F11C64");

            entity.ToTable("MONHOC");

            entity.Property(e => e.TenMonHoc).HasMaxLength(100);
        });

        modelBuilder.Entity<Namhoc>(entity =>
        {
            entity.HasKey(e => e.MaNamHoc).HasName("PK__NAMHOC__7DBADD743D8D10A9");

            entity.ToTable("NAMHOC");

            entity.Property(e => e.TenNamHoc).HasMaxLength(50);
        });

        modelBuilder.Entity<PhancongGiangday>(entity =>
        {
            entity.HasKey(e => e.MaPc).HasName("PK__PHANCONG__2725E7E5BABC98C0");

            entity.ToTable("PHANCONG_GIANGDAY");

            entity.Property(e => e.MaPc).HasColumnName("MaPC");
            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.MaHk).HasColumnName("MaHK");

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaGv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG_G__MaGV__48CFD27E");

            entity.HasOne(d => d.MaHkNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaHk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG_G__MaHK__4BAC3F29");

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG___MaLop__4AB81AF0");

            entity.HasOne(d => d.MaMonHocNavigation).WithMany(p => p.PhancongGiangdays)
                .HasForeignKey(d => d.MaMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PHANCONG___MaMon__49C3F6B7");
        });

        modelBuilder.Entity<Phonghoc>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__PHONGHOC__20BD5E5BB537698A");

            entity.ToTable("PHONGHOC");

            entity.Property(e => e.TenPhong).HasMaxLength(50);
            entity.Property(e => e.ViTri).HasMaxLength(100);
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.MaTk).HasName("PK__TAIKHOAN__2725007073994027");

            entity.ToTable("TAIKHOAN");

            entity.HasIndex(e => e.TenDangNhap, "UQ__TAIKHOAN__55F68FC04C0663BA").IsUnique();

            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.MaGv).HasColumnName("MaGV");
            entity.Property(e => e.MaHs).HasColumnName("MaHS");
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.TenDangNhap).HasMaxLength(50);
            entity.Property(e => e.VaiTro).HasMaxLength(20);

            entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.MaGv)
                .HasConstraintName("FK__TAIKHOAN__MaGV__5FB337D6");

            entity.HasOne(d => d.MaHsNavigation).WithMany(p => p.Taikhoans)
                .HasForeignKey(d => d.MaHs)
                .HasConstraintName("FK__TAIKHOAN__MaHS__5EBF139D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
