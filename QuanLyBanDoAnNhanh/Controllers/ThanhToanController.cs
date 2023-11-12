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
    [Route("api/thanhtoan")]
    [ApiController]
    public class ThanhToanController : ControllerBase
    {
        private readonly IThanhToanRepository _thanhtoan;
        public ThanhToanController(IThanhToanRepository thanhtoan)
        {
            _thanhtoan = thanhtoan;
        }

        [HttpPost("thanhtoandonhanginsertorupdate")]
        public async Task<IActionResult> ThanhToanDonHangInsertOrUpdate(ThanhToanDonHangViewModel obj)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                long result = await _thanhtoan.ThanhToanDonHangInsertOrUpdate(obj, user.ID_TaiKhoan, user.TenDangNhap);
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
    }
}
