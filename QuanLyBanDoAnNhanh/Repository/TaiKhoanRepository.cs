using Dapper;
using Newtonsoft.Json;
using QuanLyBanDoAnNhanh.Context;
using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Repository
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly DapperContext _context;
        public TaiKhoanRepository(DapperContext context)
        {
            _context = context;
        }
        private static string Encrypt(string toEncrypt)
        {

            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            const string key = "!hoang@tuan#son$";
            keyArray = UTF8Encoding.UTF8.GetBytes(key);
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }

        private static string Decrypt(string cipherString)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //Get your key from config file to open the lock!

            string key = "!hoang@tuan#son$";

            keyArray = UTF8Encoding.UTF8.GetBytes(key);


            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock
                    (toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public async Task<ResponseResultViewModel> DoiMatKhau(int ID_TaiKhoan, string MatKhauCu, string MatKhauMoi)
        {
            try
            {
                var procedureName = "DM_TaiKhoan_DoiMatKhau";
                var parameters = new DynamicParameters();
                parameters.Add("ID_TaiKhoan", ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);
                parameters.Add("MatKhauCu", Encrypt(MatKhauCu), DbType.String, ParameterDirection.Input);
                parameters.Add("MatKhauMoi", Encrypt(MatKhauMoi), DbType.String, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    ResponseResultViewModel result = await connection.QueryFirstOrDefaultAsync<ResponseResultViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("TaiKhoan_DoiMatKhau", ex);
            }
        }

        public async Task<ResponseResultViewModel> TaiKhoanInsertOrUpDate(TaiKhoanViewModel obj)
        {
            try
            {
                var procedureName = "DM_TaiKhoan_Insert_Or_Update";
                var parameters = new DynamicParameters();
                parameters.Add("ID_TaiKhoan", obj.ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);
                parameters.Add("TenDayDu", obj.TenDayDu, DbType.String, ParameterDirection.Input);
                parameters.Add("TenDangNhap", obj.TenDangNhap, DbType.String, ParameterDirection.Input);
                parameters.Add("MatKhau", Encrypt(obj.MatKhau), DbType.String, ParameterDirection.Input);
                parameters.Add("IsKhachHang", obj.IsKhachHang, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("GioiTinh", obj.GioiTinh, DbType.String, ParameterDirection.Input);
                parameters.Add("Email", obj.Email, DbType.String, ParameterDirection.Input);
                parameters.Add("SoDienThoai", obj.SoDienThoai, DbType.String, ParameterDirection.Input);
                parameters.Add("ID_ChucDanh", obj.ID_ChucDanh, DbType.Int32, ParameterDirection.Input);
                parameters.Add("ID_TinhThanh", obj.ID_TinhThanh, DbType.Int32, ParameterDirection.Input);
                parameters.Add("ID_QuanHuyen", obj.ID_QuanHuyen, DbType.Int32, ParameterDirection.Input);
                parameters.Add("ID_PhuongXa", obj.ID_PhuongXa, DbType.Int32, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseResultViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("TaiKhoan_DoiMatKhau", ex);
            }
        }

        public async Task<ResponseResultViewModel> signup(TaiKhoanViewModel obj)
        {
            try
            {
                var procedureName = "DM_TaiKhoan_Insert_Or_Update";
                var parameters = new DynamicParameters();
                parameters.Add("TenDayDu", obj.TenDayDu, DbType.String, ParameterDirection.Input);
                parameters.Add("TenDangNhap", obj.TenDangNhap, DbType.String, ParameterDirection.Input);
                parameters.Add("MatKhau", Encrypt(obj.MatKhau), DbType.String, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseResultViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("DM_TaiKhoan_Insert_Or_Update", ex);
            }
        }

        public async Task<ResponseResultViewModel> khoaTaiKhoan(int ID_TaiKhoan, bool IsLock, string NguoiCapNhat)
        {
            try
            {
                var procedureName = "KhoaTaiKhoan";
                var parameters = new DynamicParameters();
                parameters.Add("ID_TaiKhoan", ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);
                parameters.Add("IsLock", IsLock, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseResultViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("KhoaTaiKhoan", ex);
            }
        }

        public async Task<ResponseResultViewModel> xoaTaiKhoan(string ListID, string NguoiCapNhat)
        {
            try
            {
                var procedureName = "DM_TaiKhoan_Delete";
                var parameters = new DynamicParameters();
                parameters.Add("ListID", ListID, DbType.String, ParameterDirection.Input);
                parameters.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstOrDefaultAsync<ResponseResultViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("KhoaTaiKhoan", ex);
            }
        }

        public async Task<List<TaiKhoanViewModel>> GetListTaiKhoanKhachHang(int ID_TaiKhoan, string TenDayDu, string TenDangNhap, int IsLock)
        {
            var procedureName = "DM_TaiKhoan_GetList_KhachHang";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_TaiKhoan", ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);
                parameters.Add("TenDayDu", TenDayDu, DbType.String, ParameterDirection.Input);
                parameters.Add("TenDangNhap", TenDangNhap, DbType.String, ParameterDirection.Input);
                parameters.Add("IsLock", IsLock, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    var list = await connection.QueryAsync<TaiKhoanViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(procedureName, ex);
            }
        }

        public async Task<List<TaiKhoanViewModel>> GetListTaiKhoanNhanVien(int ID_TaiKhoan, string TenDayDu, string TenDangNhap, int IsLock)
        {
            var procedureName = "DM_TaiKhoan_GetList_NhanVien";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_TaiKhoan", ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);
                parameters.Add("TenDayDu", TenDayDu, DbType.String, ParameterDirection.Input);
                parameters.Add("TenDangNhap", TenDangNhap, DbType.String, ParameterDirection.Input);
                parameters.Add("IsLock", IsLock, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    var list = await connection.QueryAsync<TaiKhoanViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(procedureName, ex);
            }
        }

        public async Task<List<TaiKhoanViewModel>> GetListTaiKhoan(int ID_TaiKhoan, string TenDayDu, string TenDangNhap, int IsLock, int IsKhachHang)
        {
            var procedureName = "DM_TaiKhoan_GetList";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_TaiKhoan", ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);
                parameters.Add("TenDayDu", TenDayDu, DbType.String, ParameterDirection.Input);
                parameters.Add("TenDangNhap", TenDangNhap, DbType.String, ParameterDirection.Input);
                parameters.Add("IsLock", IsLock, DbType.Int32, ParameterDirection.Input);
                parameters.Add("IsKhachHang", IsKhachHang, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    var list = await connection.QueryAsync<TaiKhoanViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return list.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(procedureName, ex);
            }
        }

        public async Task<TaiKhoanViewModel> GetInfoTaiKhoanByID(int ID_TaiKhoan)
        {
            var procedureName = "DM_TaiKhoan_Get_By_ID";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_TaiKhoan", ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryFirstOrDefaultAsync<TaiKhoanViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(procedureName, ex);
            }
        }
    }
}
