using Dapper;
using QuanLyBanDoAnNhanh.Context;
using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Repository
{
	public class GioHangRepository : IGioHangRepository
	{
		private readonly DapperContext _context;
		public GioHangRepository(DapperContext context)
		{
			_context = context;
		}

        public async Task<List<DonHangViewModel>> GetListDonHangByIDTaiKhoan(int ID_TaiKhoan)
        {
            var procedureName = "DonHang_GetList_By_ID_TaiKhoan";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_TaiKhoan", ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<DonHangViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(procedureName, ex);
            }
        }

        public async Task<int> GetSoLuongDonHangTrongGio(int ID_TaiKhoan)
        {
            var procedureName = "DonHang_Get_Count";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_TaiKhoan", ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.ExecuteScalarAsync<int>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(procedureName, ex);
            }
        }

        public async Task<ResponseResultViewModel> DonHangInsertOrUpdate(DonHangViewModel obj, string NguoiCapNhat)
        {
            var procedureName = "DonHang_Insert_Or_Update";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_DonHang", obj.ID_DonHang, DbType.Int64, ParameterDirection.Input);
                parameters.Add("ID_TaiKhoan", obj.ID_TaiKhoan, DbType.Int32, ParameterDirection.Input);
                parameters.Add("ID_MonAn", obj.ID_MonAn, DbType.Int32, ParameterDirection.Input);
                parameters.Add("SoLuong", obj.SoLuong, DbType.Int32, ParameterDirection.Input);
                parameters.Add("Type", obj.Type, DbType.String, ParameterDirection.Input);
                parameters.Add("NguoiCapNhat", NguoiCapNhat, DbType.String, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    ResponseResultViewModel result = await connection.QueryFirstOrDefaultAsync<ResponseResultViewModel>
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
