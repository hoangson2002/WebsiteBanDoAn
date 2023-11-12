using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels.DanhMuc
{
    public class DanhMucTaiKhoanViewModel
    {
        public long ID_TaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string TenDayDu { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public Boolean IsLock { get; set; }
        public string TrangThaiTaiKhoan { get; set; }
        public Boolean IsAdmin { get; set; }
        public string LoaiTaiKhoan { get; set; }
        public Boolean IsHoatDong { get; set; }
        public string HoatDong { get; set; }
        public DateTime NgayDangKy { get; set; }
    }
}
