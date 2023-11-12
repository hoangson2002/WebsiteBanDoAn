using QuanLyBanDoAnNhanh.ExtendModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.RepoContracts
{
    public interface ITaiKhoanRepository
    {
        public Task<ResponseResultViewModel> DoiMatKhau(int ID_TaiKhoan, string MatKhauCu, string MatKhauMoi);
        public Task<ResponseResultViewModel> TaiKhoanInsertOrUpDate(TaiKhoanViewModel obj);
        public Task<ResponseResultViewModel> signup(TaiKhoanViewModel obj);
        public Task<ResponseResultViewModel> khoaTaiKhoan(int ID_TaiKhoan, bool IsLock, string NguoiCapNhat);
        public Task<ResponseResultViewModel> xoaTaiKhoan(string ListID, string NguoiCapNhat);
        public Task<List<TaiKhoanViewModel>> GetListTaiKhoanKhachHang(int ID_TaiKhoan, string TenDayDu, string TenDangNhap, int IsLock);
        public Task<List<TaiKhoanViewModel>> GetListTaiKhoanNhanVien(int ID_TaiKhoan, string TenDayDu, string TenDangNhap, int IsLock);
        public Task<List<TaiKhoanViewModel>> GetListTaiKhoan(int ID_TaiKhoan, string TenDayDu, string TenDangNhap, int IsLock, int IsKhachHang);
        public Task<TaiKhoanViewModel> GetInfoTaiKhoanByID(int ID_TaiKhoan);
    }
}
