-- 1. TẠO CƠ SỞ DỮ LIỆU
CREATE DATABASE CuaHangSachDB;
GO

-- 2. SỬ DỤNG CSDL VỪA TẠO
USE CuaHangSachDB;
GO

-- 3. TẠO BẢNG KHÁCH HÀNG (tbl_KhachHang)
CREATE TABLE tbl_KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    TenKH NVARCHAR(100) NOT NULL,       -- Tên dùng để đăng nhập
    MatKhau NVARCHAR(100) NOT NULL,     -- Mật khẩu
    HoTen NVARCHAR(150),                -- Tên đầy đủ
    DiaChi NVARCHAR(255),               -- Địa chỉ
    SoDienThoai VARCHAR(20)             -- Số điện thoại
);
GO

-- 4. TẠO BẢNG SÁCH (tbl_Sach)
CREATE TABLE tbl_Sach (
    MaSach INT IDENTITY(1,1) PRIMARY KEY,
    TenSach NVARCHAR(255) NOT NULL,     -- Tên sách
    DonGia DECIMAL(18, 2) NOT NULL,     -- Giá bán (dùng decimal cho tiền tệ)
    AnhBia VARCHAR(255),                -- Tên file ảnh (ví dụ: 'sach.jpg')
    SoLuongTon INT DEFAULT 0            -- Số lượng trong kho
);
GO

-- 5. TẠO BẢNG ĐƠN ĐẶT HÀNG (DonDatHang)
CREATE TABLE DonDatHang (
    MaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaKH INT NOT NULL,
    NgayDat DATETIME DEFAULT GETDATE(), -- Tự động lấy ngày giờ hiện tại
    NgayGiao DATETIME,                  -- Ngày hẹn giao
    TinhTrangGiaoHang INT DEFAULT 0,    -- 0: Mới đặt, 1: Đang giao, 2: Đã giao
    DaThanhToan BIT DEFAULT 0,          -- 0: Chưa thanh toán, 1: Đã thanh toán
    
    -- Tạo khóa ngoại liên kết tới bảng Khách Hàng
    FOREIGN KEY (MaKH) REFERENCES tbl_KhachHang(MaKH)
);
GO

-- 6. TẠO BẢNG CHI TIẾT ĐƠN HÀNG (ChiTietDonHang)
CREATE TABLE ChiTietDonHang (
    MaDon INT NOT NULL,
    MaSach INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18, 2) NOT NULL,     -- Lưu lại giá tại thời điểm mua
    
    -- Tạo khóa chính gồm cả MaDon và MaSach
    PRIMARY KEY (MaDon, MaSach),
    
    -- Tạo các khóa ngoại
    FOREIGN KEY (MaDon) REFERENCES DonDatHang(MaDon),
    FOREIGN KEY (MaSach) REFERENCES tbl_Sach(MaSach)
);
GO

-- 7. CHÈN DỮ LIỆU MẪU ĐỂ CHẠY THỬ
PRINT 'Chen du lieu mau...';

-- Chèn 1 khách hàng mẫu
INSERT INTO tbl_KhachHang (TenKH, MatKhau, HoTen, DiaChi, SoDienThoai)
VALUES 
(N'user', N'123', N'Nguyễn Văn A', N'123 Đường ABC, Quận 1, TPHCM', N'0909123456');

-- Chèn 4 quyển sách mẫu
INSERT INTO tbl_Sach (TenSach, DonGia, AnhBia, SoLuongTon)
VALUES
(N'Lập trình C# Toàn tập', 150000.00, N'sach-csharp.jpg', 100),
(N'ASP.NET MVC 5 từ Cơ bản đến Nâng cao', 220000.00, N'sach-mvc.jpg', 50),
(N'SQL Server cho người mới bắt đầu', 180000.00, N'sach-sql.jpg', 75),
(N'Cấu trúc dữ liệu và Giải thuật', 135000.00, N'sach-giaithuat.jpg', 120);

