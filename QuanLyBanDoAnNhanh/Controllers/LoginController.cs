using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuanLyBanDoAnNhanh.ExtendModels.Login;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ECLAIM_BAOMINH_WEB_API.Helpers;

namespace QuanLyBanDoAnNhanh.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepo;
        private readonly ILogger<LoginController> _logger;


        public LoginController(ILogger<LoginController> logger,ILoginRepository loginRepo)
        {
            _logger = logger;
            _loginRepo = loginRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DauVaoDangNhapViewModel dauVaoDangNhapViewModel)
        {
            try
            {
                DauRaDangNhapViewModel dauRaDangNhapViewModel = await _loginRepo.DangNhap(dauVaoDangNhapViewModel);
 
                if (dauRaDangNhapViewModel.erCode == 1)
                    return Ok(new { flag = true, msg = "Đăng nhập thành công", value = dauRaDangNhapViewModel });
                else if (dauRaDangNhapViewModel.erCode == 0)
                    return Ok(new { flag = false, msg = "Tài khoản đã bị khóa", value = new DauRaDangNhapViewModel() });
                else if (dauRaDangNhapViewModel.erCode == 3)
                    return Ok(new { flag = true, msg = "Tài khoản đang đăng nhập ở máy khác", value = dauRaDangNhapViewModel });
                else
                    return Ok(new { flag = false, msg = "Tài khoản mật khẩu không đúng", value = new DauRaDangNhapViewModel() });
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return Ok(new { flag = false, msg ="Xảy ra lỗi trong quá trình đăng nhập", value = new DauRaDangNhapViewModel()});
            }
        }

        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken(DauVaoDangNhapViewModel dauVaoDangNhapViewModel)
        {
            try
            {
                var user = (ThongTinNguoiDungViewModel)HttpContext.Items["ThongTinNguoiDung"];
                if (user == null)
                    return Unauthorized();

                DauRaDangNhapViewModel result =  await _loginRepo.TaoTokenMoi(user);
                if (result.TaiKhoan == null)
                    return Ok(new { flag = false, msg = "Tác vụ thất bại", value = result });
                else
                    return Ok(new { flag = true, msg = "Tác vụ thành công", value = result });
            }
            catch (Exception)
            {
                return Ok(new { flag = false, msg = "Xảy ra lỗi trong quá trình tạo lại token mới", value = new DauRaDangNhapViewModel() });
            }
        }
    }

}
