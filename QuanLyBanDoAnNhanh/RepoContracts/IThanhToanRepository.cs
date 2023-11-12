using QuanLyBanDoAnNhanh.ExtendModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.RepoContracts
{
  public interface IThanhToanRepository
    {
        public Task<long> ThanhToanDonHangInsertOrUpdate(ThanhToanDonHangViewModel obj,int ID_TaiKhoan, string NguoiCapNhat);
    }
}
