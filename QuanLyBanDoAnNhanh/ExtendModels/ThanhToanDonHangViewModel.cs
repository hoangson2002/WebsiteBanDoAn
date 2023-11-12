using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels
{
	public class ThanhToanDonHangViewModel
	{
		public int ID_KhuyenMai { get; set; }
		public int ID_HinhThuc_ThanhToan { get; set; }
		public string NguoiNhan_HoTen { get; set; }
		public string NguoiNhan_SoDienThoai { get; set; }
		public decimal TongTienDonHang { get; set; }
		public bool IsThanhToan { get; set; }
		public string SoNha { get; set; }
		public string DiaChiNhanHang { get; set; }
		public int ID_TinhThanh { get; set; }
		public int ID_QuanHuyen { get; set; }
		public int ID_PhuongXa { get; set; }
		public string GhiChu { get; set; }
		public string listID_DonHang { get; set; }
	}
}
