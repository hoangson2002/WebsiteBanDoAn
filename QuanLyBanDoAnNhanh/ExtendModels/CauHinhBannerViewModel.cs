using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.ExtendModels.DanhMuc
{
	public class CauHinhBannerViewModel
	{
		public int ID_Banner { get; set; }
		public string LienKet { get; set; }
		public DateTime? NgayBatDau { get; set; }
		public DateTime? NgayKetThuc { get; set; }
		public string NoiDung { get; set; }
		public string TieuDe { get; set; }
		public string TrangThai { get; set; }
		public string URL { get; set; }
	}
}
