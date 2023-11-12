using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels
{
	public class GioHangViewModel
	{
		public long ID_DonHang { get; set; }
		public int ID_TaiKhoan { get; set; }
		public long ID_MonAn { get; set; }
		public string TenMonAn { get; set; }
		public int SoLuong { get; set; }
		public float DonGia { get; set; }
		public float ThanhTien { get; set; }
		public int ID_TrangThai_DonHang { get; set; }
		public string TenTrangThai { get; set; }
	}
}
