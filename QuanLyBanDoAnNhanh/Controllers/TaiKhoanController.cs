using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.ExtendModels.Login;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Controllers
{
    [Route("api/taikhoan")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanRepository _taikhoan;
        public TaiKhoanController(ITaiKhoanRepository taikhoan)
        {
            _taikhoan = taikhoan;
        }

        [HttpPost("tai-khoan-insert-or-update")]
        public async Task<IActionResult> TaiKhoanInsertOrUpDate(TaiKhoanViewModel obj)
        {
            try
            {

                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                ResponseResultViewModel result = await _taikhoan.TaiKhoanInsertOrUpDate(obj);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("tai-khoan-danh-sach-thong-tin", ex);
            }
        }

        [HttpPost("khoaTaiKhoan")]
        public async Task<IActionResult> signup(int ID_TaiKhoan, bool IsLock)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                ResponseResultViewModel result = await _taikhoan.khoaTaiKhoan(ID_TaiKhoan, IsLock, user.TenDangNhap);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("khoaTaiKhoan", ex);
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> signup(TaiKhoanViewModel obj)
        {
            try
            {
                ResponseResultViewModel result = await _taikhoan.signup(obj);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("signup", ex);
            }
        }

        [HttpGet("doimatkhau")]
        public async Task<IActionResult> DoiMatKhau(string MatKhauCu, string MatKhauMoi)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                ResponseResultViewModel result = await _taikhoan.DoiMatKhau(user.ID_TaiKhoan, MatKhauCu, MatKhauMoi);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new { flag = false, severity = "warn", detail = "Thông báo", msg = "Lỗi! Vui lòng kiểm tra kết nối của bạn", log_Error = ex.ToString() });
            }
        }

        [HttpGet("xoaTaiKhoan")]
        public async Task<IActionResult> xoaTaiKhoan(string ListID)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                ResponseResultViewModel result = await _taikhoan.xoaTaiKhoan(ListID, user.TenDangNhap);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(new { flag = false, severity = "warn", detail = "Thông báo", msg = "Lỗi! Vui lòng kiểm tra kết nối của bạn", log_Error = ex.ToString() });
            }
        }

        [HttpGet("getlisttaikhoannhanvien")]
        public async Task<IActionResult> GetListTaiKhoanNhanVien(int ID_TaiKhoan, string TenDayDu, string TenDangNhap, int IsLock)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                List<TaiKhoanViewModel> result = await _taikhoan.GetListTaiKhoanNhanVien(ID_TaiKhoan, TenDayDu, TenDangNhap, IsLock);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("getlisttaikhoannhanvien", ex);
            }
        }

        [HttpGet("getlisttaikhoankhachhang")]
        public async Task<IActionResult> GetListTaiKhoanKhachHang(int ID_TaiKhoan, string TenDayDu, string TenDangNhap, int IsLock)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                List<TaiKhoanViewModel> result = await _taikhoan.GetListTaiKhoanKhachHang(ID_TaiKhoan, TenDayDu, TenDangNhap, IsLock);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("getlisttaikhoankhachhang", ex);
            }
        }

        [HttpGet("getlisttaikhoan")]
        public async Task<IActionResult> GetListTaiKhoan(int ID_TaiKhoan, string TenDayDu, string TenDangNhap, int IsLock, int IsKhachHang)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                List<TaiKhoanViewModel> result = await _taikhoan.GetListTaiKhoan(ID_TaiKhoan, TenDayDu, TenDangNhap, IsLock, IsKhachHang);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("getlisttaikhoannhanvien", ex);
            }
        }


        [HttpGet("getinfotaikhoanbyid")]
        public async Task<IActionResult> GetInfoTaiKhoanByID(int ID_TaiKhoan)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                TaiKhoanViewModel result = await _taikhoan.GetInfoTaiKhoanByID(ID_TaiKhoan);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("getinfotaikhoanbyid", ex);
            }
        }
    }
}
