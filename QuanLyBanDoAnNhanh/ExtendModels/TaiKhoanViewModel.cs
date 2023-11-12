using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels
{
	public class TaiKhoanViewModel
	{
		public int ID_TaiKhoan { get; set; }
		public string TenDayDu { get; set; }
		public string TenDangNhap { get; set; }
		public string MatKhau { get; set; }
		public string SoDienThoai { get; set; }
		public string Email { get; set; }
		public bool IsKhachHang { get; set; }
		public string Roles { get; set; }
		public string TrangThai { get; set; }
		public string GioiTinh { get; set; }
		public string TenGioiTinh { get; set; }
		public bool IsLock { get; set; }
		public string SoNha { get; set; }
		public int ID_ChucDanh { get; set; }
		public string TenChucDanh { get; set; }
		public int ID_TinhThanh { get; set; }
		public string TenTinhThanh { get; set; }
		public int ID_QuanHuyen { get; set; }
		public string TenQuanHuyen { get; set; }
		public int ID_PhuongXa { get; set; }
		public string TenPhuongXa { get; set; }
		public string DiaChi { get; set; }
	}
}
