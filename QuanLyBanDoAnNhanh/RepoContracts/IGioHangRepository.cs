using QuanLyBanDoAnNhanh.ExtendModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.RepoContracts
{
	public interface IGioHangRepository
	{
		public Task<List<DonHangViewModel>> GetListDonHangByIDTaiKhoan(int ID_TaiKhoan);
		public Task<int> GetSoLuongDonHangTrongGio(int ID_TaiKhoan);
		public Task<ResponseResultViewModel> DonHangInsertOrUpdate(DonHangViewModel obj, string NguoiCapNhat);
	}
}
