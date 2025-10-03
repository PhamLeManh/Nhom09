using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MdpProject.Models;

public partial class Project2Context : DbContext
{
    public Project2Context()
    {
    }

    public Project2Context(DbContextOptions<Project2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<MdpCamera> MdpCameras { get; set; }

    public virtual DbSet<MdpCart> MdpCarts { get; set; }

    public virtual DbSet<MdpCategory> MdpCategories { get; set; }

    public virtual DbSet<MdpChatSupport> MdpChatSupports { get; set; }

    public virtual DbSet<MdpChiTietDonHang> MdpChiTietDonHangs { get; set; }

    public virtual DbSet<MdpChiTietHoaDon> MdpChiTietHoaDons { get; set; }

    public virtual DbSet<MdpDoanhThuSanPham> MdpDoanhThuSanPhams { get; set; }

    public virtual DbSet<MdpDonHang> MdpDonHangs { get; set; }

    public virtual DbSet<MdpHoaDon> MdpHoaDons { get; set; }

    public virtual DbSet<MdpKhoHang> MdpKhoHangs { get; set; }

    public virtual DbSet<MdpKhuyenMai> MdpKhuyenMais { get; set; }

    public virtual DbSet<MdpLaptop> MdpLaptops { get; set; }

    public virtual DbSet<MdpLinhKien> MdpLinhKiens { get; set; }

    public virtual DbSet<MdpMauSac> MdpMauSacs { get; set; }

    public virtual DbSet<MdpMonitor> MdpMonitors { get; set; }

    public virtual DbSet<MdpMouse> MdpMouses { get; set; }

    public virtual DbSet<MdpPc> MdpPcs { get; set; }

    public virtual DbSet<MdpPhone> MdpPhones { get; set; }

    public virtual DbSet<MdpProductVariant> MdpProductVariants { get; set; }

    public virtual DbSet<MdpReview> MdpReviews { get; set; }

    public virtual DbSet<MdpSanPham> MdpSanPhams { get; set; }

    public virtual DbSet<MdpUser> MdpUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOPDUONG;Database=project2;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MdpCamera>(entity =>
        {
            entity.HasKey(e => e.MdpCameraId).HasName("PK__MDP_Came__75D2346CC3A86FC8");

            entity.ToTable("MDP_Camera");

            entity.Property(e => e.MdpCameraId).HasColumnName("MDP_CameraID");
            entity.Property(e => e.MdpAnh)
                .HasMaxLength(255)
                .HasColumnName("MDP_Anh");
            entity.Property(e => e.MdpBoNho)
                .HasMaxLength(50)
                .HasColumnName("MDP_BoNho");
            entity.Property(e => e.MdpCamBien)
                .HasMaxLength(50)
                .HasColumnName("MDP_CamBien");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpDoPhanGiai)
                .HasMaxLength(50)
                .HasColumnName("MDP_DoPhanGiai");
            entity.Property(e => e.MdpOngKinh)
                .HasMaxLength(100)
                .HasColumnName("MDP_OngKinh");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpTenCamera)
                .HasMaxLength(150)
                .HasColumnName("MDP_TenCamera");
            entity.Property(e => e.MdpUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_UpdatedAt");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpCameras)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Camera_SanPham");
        });

        modelBuilder.Entity<MdpCart>(entity =>
        {
            entity.HasKey(e => e.MdpCartId).HasName("PK__MDP_Cart__09A4EC5601635B8C");

            entity.ToTable("MDP_Cart");

            entity.Property(e => e.MdpCartId).HasColumnName("MDP_CartID");
            entity.Property(e => e.MdpNgayThem)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_NgayThem");
            entity.Property(e => e.MdpSoLuong)
                .HasDefaultValue(1)
                .HasColumnName("MDP_SoLuong");
            entity.Property(e => e.MdpUserId).HasColumnName("MDP_UserID");
            entity.Property(e => e.MdpVariantId).HasColumnName("MDP_VariantID");

            entity.HasOne(d => d.MdpUser).WithMany(p => p.MdpCarts)
                .HasForeignKey(d => d.MdpUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_User");

            entity.HasOne(d => d.MdpVariant).WithMany(p => p.MdpCarts)
                .HasForeignKey(d => d.MdpVariantId)
                .HasConstraintName("FK_Cart_Variant");
        });

        modelBuilder.Entity<MdpCategory>(entity =>
        {
            entity.HasKey(e => e.MdpCategoryId).HasName("PK__MDP_Cate__D0B7F9AAADD9AF8B");

            entity.ToTable("MDP_Category");

            entity.Property(e => e.MdpCategoryId).HasColumnName("MDP_CategoryID");
            entity.Property(e => e.MdpAnhDanhMuc)
                .HasMaxLength(255)
                .HasColumnName("MDP_AnhDanhMuc");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpMoTa)
                .HasMaxLength(255)
                .HasColumnName("MDP_MoTa");
            entity.Property(e => e.MdpTenDanhMuc)
                .HasMaxLength(100)
                .HasColumnName("MDP_TenDanhMuc");
        });

        modelBuilder.Entity<MdpChatSupport>(entity =>
        {
            entity.HasKey(e => e.MdpChatId).HasName("PK__MDP_Chat__82809F598F6FD60A");

            entity.ToTable("MDP_ChatSupport");

            entity.Property(e => e.MdpChatId).HasColumnName("MDP_ChatID");
            entity.Property(e => e.MdpDaXem)
                .HasDefaultValue(false)
                .HasColumnName("MDP_DaXem");
            entity.Property(e => e.MdpDonHangId).HasColumnName("MDP_DonHangID");
            entity.Property(e => e.MdpFileDinhKem)
                .HasMaxLength(255)
                .HasColumnName("MDP_FileDinhKem");
            entity.Property(e => e.MdpSenderId).HasColumnName("MDP_SenderID");
            entity.Property(e => e.MdpSentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_SentAt");
            entity.Property(e => e.MdpTinNhan).HasColumnName("MDP_TinNhan");

            entity.HasOne(d => d.MdpDonHang).WithMany(p => p.MdpChatSupports)
                .HasForeignKey(d => d.MdpDonHangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatSupport_DonHang");

            entity.HasOne(d => d.MdpSender).WithMany(p => p.MdpChatSupports)
                .HasForeignKey(d => d.MdpSenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatSupport_User");
        });

        modelBuilder.Entity<MdpChiTietDonHang>(entity =>
        {
            entity.HasKey(e => e.MdpChiTietDonHangId).HasName("PK__MDP_ChiT__BE0C36882D610B07");

            entity.ToTable("MDP_ChiTietDonHang");

            entity.Property(e => e.MdpChiTietDonHangId).HasColumnName("MDP_ChiTietDonHangID");
            entity.Property(e => e.MdpDonHangId).HasColumnName("MDP_DonHangID");
            entity.Property(e => e.MdpGia)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MDP_Gia");
            entity.Property(e => e.MdpGiamGia)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MDP_GiamGia");
            entity.Property(e => e.MdpSoLuong).HasColumnName("MDP_SoLuong");
            entity.Property(e => e.MdpThanhTien)
                .HasComputedColumnSql("([MDP_SoLuong]*[MDP_Gia]-isnull([MDP_GiamGia],(0)))", true)
                .HasColumnType("decimal(30, 2)")
                .HasColumnName("MDP_ThanhTien");
            entity.Property(e => e.MdpVariantId).HasColumnName("MDP_VariantID");

            entity.HasOne(d => d.MdpDonHang).WithMany(p => p.MdpChiTietDonHangs)
                .HasForeignKey(d => d.MdpDonHangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_DonHang");

            entity.HasOne(d => d.MdpVariant).WithMany(p => p.MdpChiTietDonHangs)
                .HasForeignKey(d => d.MdpVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_Variant");
        });

        modelBuilder.Entity<MdpChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.MdpChiTietHdid).HasName("PK__MDP_ChiT__C34216A281E9CD18");

            entity.ToTable("MDP_ChiTietHoaDon", tb => tb.HasTrigger("trg_UpdateMDP_DoanhThuSanPham"));

            entity.Property(e => e.MdpChiTietHdid).HasColumnName("MDP_ChiTietHDID");
            entity.Property(e => e.MdpGia)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MDP_Gia");
            entity.Property(e => e.MdpGiamGia)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MDP_GiamGia");
            entity.Property(e => e.MdpHoaDonId).HasColumnName("MDP_HoaDonID");
            entity.Property(e => e.MdpSoLuong).HasColumnName("MDP_SoLuong");
            entity.Property(e => e.MdpThanhTien)
                .HasComputedColumnSql("([MDP_SoLuong]*[MDP_Gia]-isnull([MDP_GiamGia],(0)))", true)
                .HasColumnType("decimal(30, 2)")
                .HasColumnName("MDP_ThanhTien");
            entity.Property(e => e.MdpVariantId).HasColumnName("MDP_VariantID");

            entity.HasOne(d => d.MdpHoaDon).WithMany(p => p.MdpChiTietHoaDons)
                .HasForeignKey(d => d.MdpHoaDonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHoaDon_HoaDon");

            entity.HasOne(d => d.MdpVariant).WithMany(p => p.MdpChiTietHoaDons)
                .HasForeignKey(d => d.MdpVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHoaDon_Variant");
        });

        modelBuilder.Entity<MdpDoanhThuSanPham>(entity =>
        {
            entity.HasKey(e => e.MdpDoanhThuId).HasName("PK__MDP_Doan__649413271B677016");

            entity.ToTable("MDP_DoanhThuSanPham");

            entity.Property(e => e.MdpDoanhThuId).HasColumnName("MDP_DoanhThuID");
            entity.Property(e => e.MdpNam).HasColumnName("MDP_Nam");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpThang).HasColumnName("MDP_Thang");
            entity.Property(e => e.MdpTongDoanhThu)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MDP_TongDoanhThu");
            entity.Property(e => e.MdpTongSoLuongBan)
                .HasDefaultValue(0)
                .HasColumnName("MDP_TongSoLuongBan");
            entity.Property(e => e.MdpVariantId).HasColumnName("MDP_VariantID");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpDoanhThuSanPhams)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DoanhThu_SanPham");

            entity.HasOne(d => d.MdpVariant).WithMany(p => p.MdpDoanhThuSanPhams)
                .HasForeignKey(d => d.MdpVariantId)
                .HasConstraintName("FK_DoanhThu_Variant");
        });

        modelBuilder.Entity<MdpDonHang>(entity =>
        {
            entity.HasKey(e => e.MdpDonHangId).HasName("PK__MDP_DonH__4909816E3A893E53");

            entity.ToTable("MDP_DonHang");

            entity.Property(e => e.MdpDonHangId).HasColumnName("MDP_DonHangID");
            entity.Property(e => e.MdpDiaChiGiaoHang)
                .HasMaxLength(255)
                .HasColumnName("MDP_DiaChiGiaoHang");
            entity.Property(e => e.MdpGhiChu).HasColumnName("MDP_GhiChu");
            entity.Property(e => e.MdpKhachHangId).HasColumnName("MDP_KhachHangID");
            entity.Property(e => e.MdpKmid).HasColumnName("MDP_KMID");
            entity.Property(e => e.MdpNgayDatDonHang)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_NgayDatDonHang");
            entity.Property(e => e.MdpNgayGiaoHang)
                .HasColumnType("datetime")
                .HasColumnName("MDP_NgayGiaoHang");
            entity.Property(e => e.MdpPhuongThucThanhToan)
                .HasMaxLength(50)
                .HasColumnName("MDP_PhuongThucThanhToan");
            entity.Property(e => e.MdpStatus)
                .HasMaxLength(20)
                .HasDefaultValue("pending")
                .HasColumnName("MDP_Status");
            entity.Property(e => e.MdpTongTien)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MDP_TongTien");
            entity.Property(e => e.MdpTrangThaiThanhToan)
                .HasMaxLength(20)
                .HasDefaultValue("unpaid")
                .HasColumnName("MDP_TrangThaiThanhToan");

            entity.HasOne(d => d.MdpKhachHang).WithMany(p => p.MdpDonHangs)
                .HasForeignKey(d => d.MdpKhachHangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonHang_User");

            entity.HasOne(d => d.MdpKm).WithMany(p => p.MdpDonHangs)
                .HasForeignKey(d => d.MdpKmid)
                .HasConstraintName("FK_DonHang_KhuyenMai");
        });

        modelBuilder.Entity<MdpHoaDon>(entity =>
        {
            entity.HasKey(e => e.MdpHoaDonId).HasName("PK__MDP_HoaD__A7905407B4713F20");

            entity.ToTable("MDP_HoaDon");

            entity.Property(e => e.MdpHoaDonId).HasColumnName("MDP_HoaDonID");
            entity.Property(e => e.MdpDonHangId).HasColumnName("MDP_DonHangID");
            entity.Property(e => e.MdpNgayXuat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_NgayXuat");
            entity.Property(e => e.MdpPhuongThucThanhToan)
                .HasMaxLength(50)
                .HasColumnName("MDP_PhuongThucThanhToan");
            entity.Property(e => e.MdpTongTien)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MDP_TongTien");
            entity.Property(e => e.MdpTrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("chua_thanh_toan")
                .HasColumnName("MDP_TrangThai");

            entity.HasOne(d => d.MdpDonHang).WithMany(p => p.MdpHoaDons)
                .HasForeignKey(d => d.MdpDonHangId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDon_DonHang");
        });

        modelBuilder.Entity<MdpKhoHang>(entity =>
        {
            entity.HasKey(e => e.MdpKhoId).HasName("PK__MDP_KhoH__3CB2BB1456E51999");

            entity.ToTable("MDP_KhoHang");

            entity.Property(e => e.MdpKhoId).HasColumnName("MDP_KhoID");
            entity.Property(e => e.MdpNgayNhap)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_NgayNhap");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpSoLuongNhap).HasColumnName("MDP_SoLuongNhap");
            entity.Property(e => e.MdpSoLuongTon).HasColumnName("MDP_SoLuongTon");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpKhoHangs)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kho_SanPham");
        });

        modelBuilder.Entity<MdpKhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MdpKmid).HasName("PK__MDP_Khuy__DA88FF05F455F568");

            entity.ToTable("MDP_KhuyenMai");

            entity.HasIndex(e => e.MdpMaCode, "UQ__MDP_Khuy__7A78201FD91EA381").IsUnique();

            entity.Property(e => e.MdpKmid).HasColumnName("MDP_KMID");
            entity.Property(e => e.MdpGiaTri)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MDP_GiaTri");
            entity.Property(e => e.MdpMaCode)
                .HasMaxLength(50)
                .HasColumnName("MDP_MaCode");
            entity.Property(e => e.MdpMoTa)
                .HasMaxLength(255)
                .HasColumnName("MDP_MoTa");
            entity.Property(e => e.MdpNgayBatDau)
                .HasColumnType("datetime")
                .HasColumnName("MDP_NgayBatDau");
            entity.Property(e => e.MdpNgayKetThuc)
                .HasColumnType("datetime")
                .HasColumnName("MDP_NgayKetThuc");
            entity.Property(e => e.MdpPhanTram).HasColumnName("MDP_PhanTram");
            entity.Property(e => e.MdpTrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("active")
                .HasColumnName("MDP_TrangThai");
        });

        modelBuilder.Entity<MdpLaptop>(entity =>
        {
            entity.HasKey(e => e.MdpLaptopId).HasName("PK__MDP_Lapt__C37DCFC5072B186E");

            entity.ToTable("MDP_Laptop");

            entity.Property(e => e.MdpLaptopId).HasColumnName("MDP_LaptopID");
            entity.Property(e => e.MdpAnh)
                .HasMaxLength(255)
                .HasColumnName("MDP_Anh");
            entity.Property(e => e.MdpCpu)
                .HasMaxLength(100)
                .HasColumnName("MDP_CPU");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpGpu)
                .HasMaxLength(100)
                .HasColumnName("MDP_GPU");
            entity.Property(e => e.MdpHeDieuHanh)
                .HasMaxLength(50)
                .HasColumnName("MDP_HeDieuHanh");
            entity.Property(e => e.MdpManHinh)
                .HasMaxLength(100)
                .HasColumnName("MDP_ManHinh");
            entity.Property(e => e.MdpPin)
                .HasMaxLength(50)
                .HasColumnName("MDP_Pin");
            entity.Property(e => e.MdpRam)
                .HasMaxLength(50)
                .HasColumnName("MDP_RAM");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpStorage)
                .HasMaxLength(50)
                .HasColumnName("MDP_Storage");
            entity.Property(e => e.MdpTenLaptop)
                .HasMaxLength(150)
                .HasColumnName("MDP_TenLaptop");
            entity.Property(e => e.MdpUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_UpdatedAt");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpLaptops)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laptop_SanPham");
        });

        modelBuilder.Entity<MdpLinhKien>(entity =>
        {
            entity.HasKey(e => e.MdpLinhKienId).HasName("PK__MDP_Linh__F7C05EE50D0D18AB");

            entity.ToTable("MDP_LinhKien");

            entity.Property(e => e.MdpLinhKienId).HasColumnName("MDP_LinhKienID");
            entity.Property(e => e.MdpAnh)
                .HasMaxLength(255)
                .HasColumnName("MDP_Anh");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpLoaiLinhKien)
                .HasMaxLength(100)
                .HasColumnName("MDP_LoaiLinhKien");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpTenLinhKien)
                .HasMaxLength(150)
                .HasColumnName("MDP_TenLinhKien");
            entity.Property(e => e.MdpThongSoKyThuat).HasColumnName("MDP_ThongSoKyThuat");
            entity.Property(e => e.MdpUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_UpdatedAt");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpLinhKiens)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LinhKien_SanPham");
        });

        modelBuilder.Entity<MdpMauSac>(entity =>
        {
            entity.HasKey(e => e.MdpMauSacId).HasName("PK__MDP_MauS__8E77F43002F3B44C");

            entity.ToTable("MDP_MauSac");

            entity.Property(e => e.MdpMauSacId).HasColumnName("MDP_MauSacID");
            entity.Property(e => e.MdpMaHex)
                .HasMaxLength(20)
                .HasColumnName("MDP_MaHex");
            entity.Property(e => e.MdpTenMau)
                .HasMaxLength(50)
                .HasColumnName("MDP_TenMau");
        });

        modelBuilder.Entity<MdpMonitor>(entity =>
        {
            entity.HasKey(e => e.MdpMonitorId).HasName("PK__MDP_Moni__6BE1CE680DB5C264");

            entity.ToTable("MDP_Monitor");

            entity.Property(e => e.MdpMonitorId).HasColumnName("MDP_MonitorID");
            entity.Property(e => e.MdpAnh)
                .HasMaxLength(255)
                .HasColumnName("MDP_Anh");
            entity.Property(e => e.MdpCongKetNoi)
                .HasMaxLength(100)
                .HasColumnName("MDP_CongKetNoi");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpDoPhanGiai)
                .HasMaxLength(50)
                .HasColumnName("MDP_DoPhanGiai");
            entity.Property(e => e.MdpKichThuoc)
                .HasMaxLength(50)
                .HasColumnName("MDP_KichThuoc");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpTamNen)
                .HasMaxLength(50)
                .HasColumnName("MDP_TamNen");
            entity.Property(e => e.MdpTanSoQuet)
                .HasMaxLength(50)
                .HasColumnName("MDP_TanSoQuet");
            entity.Property(e => e.MdpTenMonitor)
                .HasMaxLength(150)
                .HasColumnName("MDP_TenMonitor");
            entity.Property(e => e.MdpUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_UpdatedAt");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpMonitors)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Monitor_SanPham");
        });

        modelBuilder.Entity<MdpMouse>(entity =>
        {
            entity.HasKey(e => e.MdpMouseId).HasName("PK__MDP_Mous__8B225B95F20EE160");

            entity.ToTable("MDP_Mouse");

            entity.Property(e => e.MdpMouseId).HasColumnName("MDP_MouseID");
            entity.Property(e => e.MdpAnh)
                .HasMaxLength(255)
                .HasColumnName("MDP_Anh");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpDenLed).HasColumnName("MDP_DenLED");
            entity.Property(e => e.MdpDoPhanGiai)
                .HasMaxLength(50)
                .HasColumnName("MDP_DoPhanGiai");
            entity.Property(e => e.MdpKieuKetNoi)
                .HasMaxLength(50)
                .HasColumnName("MDP_KieuKetNoi");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpSoNut).HasColumnName("MDP_SoNut");
            entity.Property(e => e.MdpTenMouse)
                .HasMaxLength(150)
                .HasColumnName("MDP_TenMouse");
            entity.Property(e => e.MdpUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_UpdatedAt");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpMouses)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mouse_SanPham");
        });

        modelBuilder.Entity<MdpPc>(entity =>
        {
            entity.HasKey(e => e.MdpPcid).HasName("PK__MDP_PC__4E66C0B0C92F3639");

            entity.ToTable("MDP_PC");

            entity.Property(e => e.MdpPcid).HasColumnName("MDP_PCID");
            entity.Property(e => e.MdpAnh)
                .HasMaxLength(255)
                .HasColumnName("MDP_Anh");
            entity.Property(e => e.MdpCaseType)
                .HasMaxLength(100)
                .HasColumnName("MDP_CaseType");
            entity.Property(e => e.MdpCpu)
                .HasMaxLength(100)
                .HasColumnName("MDP_CPU");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpGpu)
                .HasMaxLength(100)
                .HasColumnName("MDP_GPU");
            entity.Property(e => e.MdpMainboard)
                .HasMaxLength(100)
                .HasColumnName("MDP_Mainboard");
            entity.Property(e => e.MdpPsu)
                .HasMaxLength(100)
                .HasColumnName("MDP_PSU");
            entity.Property(e => e.MdpRam)
                .HasMaxLength(50)
                .HasColumnName("MDP_RAM");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpStorage)
                .HasMaxLength(50)
                .HasColumnName("MDP_Storage");
            entity.Property(e => e.MdpTenPc)
                .HasMaxLength(150)
                .HasColumnName("MDP_TenPC");
            entity.Property(e => e.MdpUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_UpdatedAt");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpPcs)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PC_SanPham");
        });

        modelBuilder.Entity<MdpPhone>(entity =>
        {
            entity.HasKey(e => e.MdpPhoneId).HasName("PK__MDP_Phon__FC37F043BF5DEBE2");

            entity.ToTable("MDP_Phone");

            entity.Property(e => e.MdpPhoneId).HasColumnName("MDP_PhoneID");
            entity.Property(e => e.MdpAnh)
                .HasMaxLength(255)
                .HasColumnName("MDP_Anh");
            entity.Property(e => e.MdpCamera)
                .HasMaxLength(100)
                .HasColumnName("MDP_Camera");
            entity.Property(e => e.MdpChipset)
                .HasMaxLength(100)
                .HasColumnName("MDP_Chipset");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpHeDieuHanh)
                .HasMaxLength(50)
                .HasColumnName("MDP_HeDieuHanh");
            entity.Property(e => e.MdpManHinh)
                .HasMaxLength(100)
                .HasColumnName("MDP_ManHinh");
            entity.Property(e => e.MdpPin)
                .HasMaxLength(50)
                .HasColumnName("MDP_Pin");
            entity.Property(e => e.MdpRam)
                .HasMaxLength(50)
                .HasColumnName("MDP_RAM");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpStorage)
                .HasMaxLength(50)
                .HasColumnName("MDP_Storage");
            entity.Property(e => e.MdpTenPhone)
                .HasMaxLength(150)
                .HasColumnName("MDP_TenPhone");
            entity.Property(e => e.MdpUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_UpdatedAt");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpPhones)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Phone_SanPham");
        });

        modelBuilder.Entity<MdpProductVariant>(entity =>
        {
            entity.HasKey(e => e.MdpVariantId).HasName("PK__MDP_Prod__1C5F47375C58F755");

            entity.ToTable("MDP_ProductVariant");

            entity.Property(e => e.MdpVariantId).HasColumnName("MDP_VariantID");
            entity.Property(e => e.MdpAnh)
                .HasMaxLength(255)
                .HasColumnName("MDP_Anh");
            entity.Property(e => e.MdpDungLuong)
                .HasMaxLength(50)
                .HasColumnName("MDP_DungLuong");
            entity.Property(e => e.MdpGia)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MDP_Gia");
            entity.Property(e => e.MdpMauSacId).HasColumnName("MDP_MauSacID");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpSoLuong)
                .HasDefaultValue(0)
                .HasColumnName("MDP_SoLuong");

            entity.HasOne(d => d.MdpMauSac).WithMany(p => p.MdpProductVariants)
                .HasForeignKey(d => d.MdpMauSacId)
                .HasConstraintName("FK_Variant_MauSac");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpProductVariants)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Variant_SanPham");
        });

        modelBuilder.Entity<MdpReview>(entity =>
        {
            entity.HasKey(e => e.MdpReviewId).HasName("PK__MDP_Revi__A56E07245B792F56");

            entity.ToTable("MDP_Review");

            entity.Property(e => e.MdpReviewId).HasColumnName("MDP_ReviewID");
            entity.Property(e => e.MdpComment).HasColumnName("MDP_Comment");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpRating).HasColumnName("MDP_Rating");
            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpUserId).HasColumnName("MDP_UserID");

            entity.HasOne(d => d.MdpSanPham).WithMany(p => p.MdpReviews)
                .HasForeignKey(d => d.MdpSanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_SanPham");

            entity.HasOne(d => d.MdpUser).WithMany(p => p.MdpReviews)
                .HasForeignKey(d => d.MdpUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_User");
        });

        modelBuilder.Entity<MdpSanPham>(entity =>
        {
            entity.HasKey(e => e.MdpSanPhamId).HasName("PK__MDP_SanP__D23933EFF0AD03A8");

            entity.ToTable("MDP_SanPham");

            entity.Property(e => e.MdpSanPhamId).HasColumnName("MDP_SanPhamID");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpMoTa)
                .HasMaxLength(255)
                .HasColumnName("MDP_MoTa");
            entity.Property(e => e.MdpTenSanPham)
                .HasMaxLength(150)
                .HasColumnName("MDP_TenSanPham");
            entity.Property(e => e.MdpUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_UpdatedAt");
        });

        modelBuilder.Entity<MdpUser>(entity =>
        {
            entity.HasKey(e => e.MdpUserId).HasName("PK__MDP_User__448A8B6FC27AC92F");

            entity.ToTable("MDP_Users");

            entity.HasIndex(e => e.MdpTenDangNhap, "UQ__MDP_User__4A8F7FE7FA9E3196").IsUnique();

            entity.HasIndex(e => e.MdpEmail, "UQ__MDP_User__C6C509D1CE31E231").IsUnique();

            entity.Property(e => e.MdpUserId).HasColumnName("MDP_UserID");
            entity.Property(e => e.MdpAvatar)
                .HasMaxLength(255)
                .HasColumnName("MDP_Avatar");
            entity.Property(e => e.MdpCreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_CreatedAt");
            entity.Property(e => e.MdpDiaChi)
                .HasMaxLength(255)
                .HasColumnName("MDP_DiaChi");
            entity.Property(e => e.MdpEmail)
                .HasMaxLength(100)
                .HasColumnName("MDP_Email");
            entity.Property(e => e.MdpGioiTinh)
                .HasMaxLength(10)
                .HasColumnName("MDP_GioiTinh");
            entity.Property(e => e.MdpHoTen)
                .HasMaxLength(100)
                .HasColumnName("MDP_HoTen");
            entity.Property(e => e.MdpMatKhau)
                .HasMaxLength(255)
                .HasColumnName("MDP_MatKhau");
            entity.Property(e => e.MdpNgaySinh).HasColumnName("MDP_NgaySinh");
            entity.Property(e => e.MdpRole)
                .HasMaxLength(20)
                .HasDefaultValue("khachhang")
                .HasColumnName("MDP_Role");
            entity.Property(e => e.MdpSoDienThoai)
                .HasMaxLength(20)
                .HasColumnName("MDP_SoDienThoai");
            entity.Property(e => e.MdpTenDangNhap)
                .HasMaxLength(50)
                .HasColumnName("MDP_TenDangNhap");
            entity.Property(e => e.MdpTrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("active")
                .HasColumnName("MDP_TrangThai");
            entity.Property(e => e.MdpUpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MDP_UpdatedAt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
