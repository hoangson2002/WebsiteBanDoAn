using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.ExtendModels.Login;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Controllers
{
	[Route("api/giohang")]
	[ApiController]
	public class GioHangController : ControllerBase
	{
        private readonly IGioHangRepository _gioHang;
        public GioHangController(IGioHangRepository gioHang)
        {
            _gioHang = gioHang;
        }

		[HttpGet("getlistdonhangbyidtaikhoan")]
		public async Task<IActionResult> GetListDonHangByIDTaiKhoan()
		{
			try
			{
				var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

				List<DonHangViewModel> result = await _gioHang.GetListDonHangByIDTaiKhoan(user.ID_TaiKhoan);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetListDonHangByIDTaiKhoan", ex);
			}
		}

		[HttpGet("getsoluongdonhangtronggio")]
		public async Task<IActionResult> GetSoLuongDonHangTrongGio()
		{
			try
			{
				var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

				int result = await _gioHang.GetSoLuongDonHangTrongGio(user.ID_TaiKhoan);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetComboboxTinhThanh", ex);
			}
		}

		[HttpPost("donhanginsertorupdate")]
		public async Task<IActionResult> DonHangInsertOrUpdate(DonHangViewModel obj)
		{
			try
			{
				var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

				obj.ID_TaiKhoan = user.ID_TaiKhoan;
				ResponseResultViewModel result = await _gioHang.DonHangInsertOrUpdate(obj, user.TenDangNhap);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("GetComboboxTinhThanh", ex);
			}
		}
	}
}
