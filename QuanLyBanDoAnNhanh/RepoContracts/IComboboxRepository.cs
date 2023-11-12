using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.ExtendModels.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.RepoContracts
{
	public interface IComboboxRepository
	{
		public Task<List<ComboboxViewModel>> GetComboboxTinhThanh();
		public Task<List<ComboboxViewModel>> GetComboboxQuanHuyen(int ID_TinhThanh);
		public Task<List<ComboboxViewModel>> GetComboboxPhuongXa(int ID_QuanHuyen);
		public Task<List<ComboboxViewModel>> GetComboboxHinhThucThanhToan();
	}
}
