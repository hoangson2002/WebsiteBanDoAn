using DoAnTotNghiep_2023.ExtendModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.ExtendModels.DanhMuc;
using QuanLyBanDoAnNhanh.ExtendModels.Login;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Controllers
{
	[Route("api/quantri")]
	[ApiController]
	public class QuanTriController : ControllerBase
	{
		private readonly IQuanTriRepository _quanTri;
		public QuanTriController(IQuanTriRepository quanTri)
		{
			_quanTri = quanTri;
		}

		#region Danh mục loại đồ ăn
		[HttpGet("getlistbannerconfig")]
        public async Task<IActionResult> GetListBannerConfig()
        {
            try
            {
				var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

                List<CauHinhBannerViewModel> list = await _quanTri.GetListBannerConfig();
                return Ok(list);

            }
            catch (Exception ex)
            {
                throw new ArgumentException("GetListBannerConfig", ex);
            }
        }

        [HttpGet("deletebannerconfig")]
        public IActionResult DeleteBannerConfig(string ListID)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

                bool result = _quanTri.DeleteBannerConfig(ListID);
				if (result)
				{
                    return Ok(new { flag = true, severity = "success", detail = "Thông báo", msg = "Tác vụ thực hiện thành công!" });
                }
				else
				{
                    return Ok(new { flag = true, severity = "warn", detail = "Thông báo", msg = "Tác vụ thực hiện thất bại!" });
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("DeleteBannerConfig", ex);
            }
        }

        [HttpPost("bannerconfiginsertorupdate")]
        public IActionResult BannerConfigInsertOrUpdate(CauHinhBannerViewModel obj)
        {
            try
            {
				var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

				int result = _quanTri.BannerConfigInsertOrUpdate(obj, user.TenDangNhap);
                if (result > 0)
                {
                    return Ok(new { flag = true, severity = "success", detail = "Thông báo", msg = "Tác vụ thực hiện thành công!" });
                }
                else
                {
                    return Ok(new { flag = true, severity = "warn", detail = "Thông báo", msg = "Tác vụ thực hiện thất bại!" });
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("DeleteBannerConfig", ex);
            }
        }

        #endregion

        #region Danh mục loại đồ ăn
        [HttpGet("getlistphanloaimonan")]
        public IActionResult GetListLoaiDoAn()
        {
            try
            {
                DataTable list = _quanTri.GetListMonAnPhanLoai();
                var json = JsonConvert.SerializeObject(list);
                return Ok(json);

            }
            catch (Exception ex)
            {
                throw new ArgumentException("GetListMonAnPhanLoai", ex);
            }
        }

        [HttpGet("deletephanloaimonan")]
        public IActionResult DeletePhanLoaiMonAn(string ListID)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

                bool result = _quanTri.DeletePhanLoaiMonAn(ListID);
                if (result)
                {
                    return Ok(new { flag = true, severity = "success", detail = "Thông báo", msg = "Tác vụ thực hiện thành công!" });
                }
                else
                {
                    return Ok(new { flag = true, severity = "warn", detail = "Thông báo", msg = "Tác vụ thực hiện thất bại!" });
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("DeleteLoaiDoAn", ex);
            }
        }

        [HttpGet("phanloaidoaninsertorupdate")]
        public IActionResult LoaiDoAnInsertOrUpdate(int ID_MonAnPhanLoai, string TenMonAnPhanLoai)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

                int result = _quanTri.PhanLoaiMonAnInsertOrUpdate(ID_MonAnPhanLoai, TenMonAnPhanLoai, "sonht");
                if (result > 0)
                {
                    return Ok(new { flag = true, severity = "success", detail = "Thông báo", msg = "Tác vụ thực hiện thành công!" });
                }
                else
                {
                    return Ok(new { flag = true, severity = "warn", detail = "Thông báo", msg = "Tác vụ thực hiện thất bại!" });
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("LoaiDoAnInsertOrUpdate", ex);
            }
        }
        #endregion

        #region  Danh mục nguyên liệu
        [HttpGet("getlistnguyenlieu")]
        public IActionResult GetListNguyenLieu()
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                DataTable list = _quanTri.GetListNguyenLieu();
                var json = JsonConvert.SerializeObject(list);
                return Ok(json);

            }
            catch (Exception ex)
            {
                throw new ArgumentException("GetListNguyenLieu", ex);
            }
        }

        [HttpGet("deletenguyenlieu")]
        public IActionResult DeleteNguyenLieu(string ListID)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

                bool result = _quanTri.DeleteNguyenLieu(ListID);
                if (result)
                {
                    return Ok(new { flag = true, severity = "success", detail = "Thông báo", msg = "Tác vụ thực hiện thành công!" });
                }
                else
                {
                    return Ok(new { flag = true, severity = "warn", detail = "Thông báo", msg = "Tác vụ thực hiện thất bại!" });
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("DeleteNguyenLieu", ex);
            }
        }

        [HttpGet("nguyenlieuinsertorupdate")]
        public IActionResult NguyenLieuAnInsertOrUpdate(int ID_NguyenLieu, string TenNguyenLieu)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

                int result = _quanTri.NguyenLieuAnInsertOrUpdate(ID_NguyenLieu, TenNguyenLieu, "sonht");
                if (result > 0)
                {
                    return Ok(new { flag = true, severity = "success", detail = "Thông báo", msg = "Tác vụ thực hiện thành công!" });
                }
                else
                {
                    return Ok(new { flag = true, severity = "warn", detail = "Thông báo", msg = "Tác vụ thực hiện thất bại!" });
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("NguyenLieuAnInsertOrUpdate", ex);
            }
        }
        #endregion
        #region Danh mục món ăn
        [HttpGet("getlistmonan")]
        public IActionResult GetListMonAn(int ID_MonAnTrangThai, int ID_MonAnPhanLoai, string TenMonAn)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

                if (TenMonAn == null)
                    TenMonAn = "";
                DataTable list = _quanTri.GetListMonAn(ID_MonAnTrangThai, ID_MonAnPhanLoai, TenMonAn);
                var json = JsonConvert.SerializeObject(list);
                return Ok(json);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("GetListMonAn", ex);
            }
        }

        [HttpPost("monaninsertorupdate")]
        public IActionResult MonAnInSertOrUpdate(MonAnViewModel obj, string TenNguyenLieu)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

                int result = _quanTri.MonAnInSertOrUpdate(obj, user.TenDangNhap);
                if (result > 0)
                {
                    return Ok(new { flag = true, severity = "success", detail = "Thông báo", msg = "Tác vụ thực hiện thành công!", value = result});
                }
                else
                {
                    return Ok(new { flag = true, severity = "warn", detail = "Thông báo", msg = "Tác vụ thực hiện thất bại!", value = result });
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("NguyenLieuAnInsertOrUpdate", ex);
            }
        }
        [HttpGet("deletemonan")]
        public IActionResult DeleteMonAn(string ListID)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

                bool result = _quanTri.DeleteMonAn(ListID);
                if (result)
                {
                    return Ok(new { flag = true, severity = "success", detail = "Thông báo", msg = "Tác vụ thực hiện thành công!" });
                }
                else
                {
                    return Ok(new { flag = true, severity = "warn", detail = "Thông báo", msg = "Tác vụ thực hiện thất bại!" });
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("DeleteNguyenLieu", ex);
            }
        }

        [HttpGet("getlistnguyenlieumonan")]
        public IActionResult GetListNguyenLieuMonAn(int ID_MonAn)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

				DataTable list = _quanTri.GetListNguyenLieuMonAn(ID_MonAn);
                var json = JsonConvert.SerializeObject(list);
                return Ok(json);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("GetListMonAn", ex);
            }
        }
        #endregion

        #region ComboBox
        [HttpGet("comboboxphanloaimonan")]
        public async Task<IActionResult> ComboboxPhanLoaiMonAn()
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();

               
                List<ComboboxViewModel> list = await _quanTri.ComboboxPhanLoaiMonAn();
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("ComboboxPhanLoaiMonAn", ex);
            }
        }

        [HttpGet("comboboxtrangthaimonan")]
        public async Task<IActionResult> ComboboxTrangThaiMonAn()
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();


                List<ComboboxViewModel> list = await _quanTri.ComboboxTrangThaiMonAn();
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("ComboboxPhanLoaiMonAn", ex);
            }
        }

        [HttpGet("comboboxnguyenlieu")]
        public async Task<IActionResult> ComboboxNguyenLieu()
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
				if (user == null)
					return Unauthorized();


                List<ComboboxViewModel> list = await _quanTri.ComboboxNguyenLieu();
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("ComboboxNguyenLieu", ex);
            }
        }
        #endregion

        #region save file
        [HttpPost("save_file")]
        public async Task<IActionResult> UploadFile_GiamDinhThietHai(int ID_MonAn)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                var file = HttpContext.Request.Form.Files;
                if (file.Count == 0)
                    return Ok(new { flag = false, msg = "Không tìm thấy thông tin file !" });

                foreach (var item in file)
                {
                    if (item.Length > 250 * 1024 * 1024)
                        return Ok(new { flag = false, msg = "File không được vượt quá 250MB !" });
                }

                HoSoAttachmentViewModel obj = await _quanTri.UploadFile(file, ID_MonAn);

                int result = await _quanTri.luuThongTinHinhAnhMonAn(obj.FilePath, ID_MonAn, user.TenDangNhap);

                return Ok(new { flag = result > 0, msg = result > 0 ? "Lưu thông tin file thành công" : "Lưu thông tin file thất bại !" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion

        #region Tìm phiếu giảm giá
        [HttpGet("timphieugiamgia")]
        public async Task<IActionResult> TimPhieuGiamGia(string MaKhuyenMai)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                PhieuGiamGiaViewModel list = await _quanTri.TimPhieuGiamGia(MaKhuyenMai);
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("TimPhieuGiamGia", ex);
            }
        }
        #endregion
    }
}
