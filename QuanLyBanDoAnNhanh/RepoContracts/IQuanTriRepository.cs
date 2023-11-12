using Microsoft.AspNetCore.Http;
using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.ExtendModels.DanhMuc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.RepoContracts
{
	public interface IQuanTriRepository
	{
		#region Cấu hình banner
		public Task<List<CauHinhBannerViewModel>> GetListBannerConfig();
		public bool DeleteBannerConfig(string ListID);
		public int BannerConfigInsertOrUpdate(CauHinhBannerViewModel obj, string NguoiCapNhat);
		#endregion
		#region Danh mục loại đồ ăn
		public DataTable GetListMonAnPhanLoai();
		public bool DeletePhanLoaiMonAn(string ListID);
		public int PhanLoaiMonAnInsertOrUpdate(int ID_MonAnPhanLoai, string TenMonAnPhanLoai, string NguoiCapNhat);
		#endregion

		#region Danh mục nguyên liệu
		public DataTable GetListNguyenLieu();
		public bool DeleteNguyenLieu(string ListID);
		public int NguyenLieuAnInsertOrUpdate(int ID_MonAnPhanLoai, string TenMonAnPhanLoai, string NguoiCapNhat);
		#endregion

		#region Danh mục món ăn
		public DataTable GetListMonAn(int ID_MonAnTrangThai, int ID_MonAnPhanLoai, string TenMonAn);
		public int MonAnInSertOrUpdate(MonAnViewModel obj, string NguoiCapNhat);
		public bool DeleteMonAn(string ListID);
		public DataTable GetListNguyenLieuMonAn(int ID_MonAn);
		#endregion

		#region Combobox
		public Task<List<ComboboxViewModel>> ComboboxPhanLoaiMonAn();
		public Task<List<ComboboxViewModel>> ComboboxTrangThaiMonAn();
		public Task<List<ComboboxViewModel>> ComboboxNguyenLieu();
		#endregion

		#region save file
		public Task<HoSoAttachmentViewModel> UploadFile(IFormFileCollection file, int ID_MonAn);
		public Task<int> luuThongTinHinhAnhMonAn(string url, int ID_MonAn, string NguoiCapNhat);

		#endregion

		#region Tìm phiếu ggiamr giá
		public Task<PhieuGiamGiaViewModel> TimPhieuGiamGia(string MaKhuyenMai);

		#endregion
	}
}
