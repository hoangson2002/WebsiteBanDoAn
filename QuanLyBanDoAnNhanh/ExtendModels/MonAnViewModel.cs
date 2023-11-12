using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels.DanhMuc
{
	public class MonAnViewModel
	{
		public int ID_MonAn { get; set; }
		public int ID_MonAnPhanLoai { get; set; }
		public int ID_MonAnTrangThai { get; set; }
		public string TenMonAn { get; set; }
		public string TenTrangThai { get; set; }
		public string TenMonAnPhanLoai { get; set; }
		public string HinhAnhTheHien { get; set; }
		public string MaGiamGia { get; set; }
		public decimal DonGia { get; set; }
		public string MoTa { get; set; }
		public string TenNguyenLieu { get; set; }
		public string ListXoaNguyenLieu { get; set; }
		public List<NguyenLieuMonAnViewModel> listNguyenLieu { get; set; }
	}
	public class NguyenLieuMonAnViewModel
	{
		public int ID { get; set; }
		public int ID_NguyenLieu { get; set; }
	}
}
