using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.RepoContracts
{
	public interface ITrangChuRepository
	{
		#region Tìm kiếm đồ ăn
		public DataTable TimKiemDoAn(int ID_MonAnPhanLoai, string TenMonAn, int SapXep);
		public DataTable GetDoAnByID(int ID_MoAn);

		#endregion

		#region Đơn hàng được mua nhiều nhất
		public DataTable MonAnDuocMuaNhieuNhat();

		#endregion

	}
}
