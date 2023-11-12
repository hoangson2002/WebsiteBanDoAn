using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyBanDoAnNhanh.ExtendModels.Login;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Controllers
{
	[Route("api/trangchu")]
	[ApiController]
	public class TrangChuController : ControllerBase
	{
		private readonly ITrangChuRepository _trangChu;
		public TrangChuController(ITrangChuRepository trangChu)
		{
			_trangChu = trangChu;
		}
        #region Tìm kiếm đồ ăn
        [HttpGet("timkiemdoan")]
        public IActionResult TimKiemDoAn(int ID_MonAnPhanLoai, string TenMonAn, int SapXep)
        {
            try
            {
                if (TenMonAn == null)
                    TenMonAn = "";
                DataTable list = _trangChu.TimKiemDoAn(ID_MonAnPhanLoai, TenMonAn, SapXep);
                var json = JsonConvert.SerializeObject(list);
                return Ok(json);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("TimKiemDoAn", ex);
            }
        }

        [HttpGet("getchitietdoanbyid")]
        public IActionResult GetDoAnByID(int ID_MonAn)
        {
            try
            {
                DataTable list = _trangChu.GetDoAnByID(ID_MonAn);
                var json = JsonConvert.SerializeObject(list);
                return Ok(json);
            }   
            catch (Exception ex)
            {
                throw new ArgumentException("TimKiemDoAn", ex);
            }
        }
        #endregion

        #region Đơn hàng được mua nhiều nhất
        [HttpGet("monanduocmuanhieunhat")]
        public IActionResult MonAnDuocMuaNhieuNhat()
        {
            try
            {
                DataTable list = _trangChu.MonAnDuocMuaNhieuNhat();
                var json = JsonConvert.SerializeObject(list);
                return Ok(json);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("MonAnDuocMuaNhieuNhat", ex);
            }
        }
        #endregion
    }
}
