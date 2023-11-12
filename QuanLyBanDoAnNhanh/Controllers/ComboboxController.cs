using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.ExtendModels.DanhMuc;
using QuanLyBanDoAnNhanh.ExtendModels.Login;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Controllers
{
	[Route("api/combobox")]
	[ApiController]
	public class ComboboxController : ControllerBase
	{
		private readonly IComboboxRepository _combobox;
		public ComboboxController(IComboboxRepository combobox)
		{
			_combobox = combobox;
		}

		[HttpGet("comboboxtinhthanh")]
		public async Task<IActionResult> GetComboboxTinhThanh()
		{
			try
			{
				var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

				List<ComboboxViewModel> list = await _combobox.GetComboboxTinhThanh();
				return Ok(list);

			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetComboboxTinhThanh", ex);
			}
		}

		[HttpGet("comboboxquanhuyen")]
		public async Task<IActionResult> GetComboboxQuanHuyen(int ID_TinhThanh)
		{
			try
			{
				var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

				List<ComboboxViewModel> list = await _combobox.GetComboboxQuanHuyen(ID_TinhThanh);
				return Ok(list);

			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetComboboxQuanHuyen", ex);
			}
		}

		[HttpGet("comboboxphuongxa")]
		public async Task<IActionResult> GetComboboxPhuongXa(int ID_QuanHuyen)
		{
			try
			{
				var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

				List<ComboboxViewModel> list = await _combobox.GetComboboxPhuongXa(ID_QuanHuyen);
				return Ok(list);

			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetComboboxPhuongXa", ex);
			}
		}

		[HttpGet("comboboxhinhthucthanhtoan")]
		public async Task<IActionResult> GetComboboxHinhThucThanhToan()
		{
			try
			{
				var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

				List<ComboboxViewModel> list = await _combobox.GetComboboxHinhThucThanhToan();
				return Ok(list);

			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetComboboxHinhThucThanhToan", ex);
			}
		}
	}
}
