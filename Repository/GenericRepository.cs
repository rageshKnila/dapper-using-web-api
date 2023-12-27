using Academy.Data;
using Academy.Model;
using Dapper;
using static Dapper.SqlMapper;
using System.Data;

namespace Academy.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILogger<GenericRepository> _logger;

        public GenericRepository(IDbConnection connection, ILogger<GenericRepository> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                return await _connection.QueryAsync<T>(sql, parms, commandType: commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location GenericRepository.QueryAsync : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                return await _connection.QueryFirstOrDefaultAsync<T>(sql, parms, commandType: commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location GenericRepository.QueryFirstOrDefaultAsync : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                return await _connection.QuerySingleOrDefaultAsync<T>(sql, parms, commandType: commandType);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location GenericRepository.QuerySingleOrDefaultAsync : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public async Task<GridReader> QueryMultipleAsync(string sql, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                return await _connection.QueryMultipleAsync(sql, parms, commandType: commandType, commandTimeout: 0);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location GenericRepository.QueryMultipleAsync : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }


}

