using Dapper;
using QuanLyBanDoAnNhanh.Context;
using QuanLyBanDoAnNhanh.ExtendModels;
using QuanLyBanDoAnNhanh.ExtendModels.DanhMuc;
using QuanLyBanDoAnNhanh.RepoContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanDoAnNhanh.Repository
{
	public class ComboboxRepository : IComboboxRepository
	{
		private readonly DapperContext _context;
		public ComboboxRepository(DapperContext context)
		{
			_context = context;
		}
        public async Task<List<ComboboxViewModel>> GetComboboxTinhThanh()
        {
            var procedureName = "ComboboxTinhThanh";
            try
            {
                var parameters = new DynamicParameters();

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<ComboboxViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(procedureName, ex);
            }
        }

        public async Task<List<ComboboxViewModel>> GetComboboxQuanHuyen(int ID_TinhThanh)
        {
            var procedureName = "ComboboxQuanHuyen";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_TinhThanh", ID_TinhThanh, DbType.Int32, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<ComboboxViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(procedureName, ex);
            }
        }

        public async Task<List<ComboboxViewModel>> GetComboboxPhuongXa(int ID_QuanHuyen)
        {
            var procedureName = "ComboboxPhuongXa";
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID_QuanHuyen", ID_QuanHuyen, DbType.Int32, ParameterDirection.Input);

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<ComboboxViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(procedureName, ex);
            }
        }

        public async Task<List<ComboboxViewModel>> GetComboboxHinhThucThanhToan()
        {
            var procedureName = "ComboboxHinhThucThanhToan";
            try
            {
                var parameters = new DynamicParameters();

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<ComboboxViewModel>
                        (procedureName, parameters, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(procedureName, ex);
            }
        }

    }
}
