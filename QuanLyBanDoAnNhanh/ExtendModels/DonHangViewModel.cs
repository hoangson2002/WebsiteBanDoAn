using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels
{
	public class DonHangViewModel
	{
		public long ID_DonHang { get; set; }
		public int ID_TaiKhoan { get; set; }
		public int ID_MonAn { get; set; }
		public int SoLuong { get; set; }
		public string Type { get; set; }
		public string TenMonAn { get; set; }
		public string HinhAnhTheHien { get; set; }
		public string DonGia { get; set; }
		public decimal ThanhTien { get; set; }
	}
}
