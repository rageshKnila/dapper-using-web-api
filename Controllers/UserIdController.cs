using Academy.Model;
using Academy.Repository;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Academy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserIdController : ControllerBase
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ILogger<UserIdController> _logger;



        public UserIdController(IGenericRepository genericRepository, ILogger<UserIdController> logger)
        {
            _genericRepository = genericRepository;
            _logger = logger;

        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sql = "dbo.GetUserIdById";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var result = await _genericRepository.QueryFirstOrDefaultAsync<UserId>(sql, parameters, CommandType.StoredProcedure);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location UserIdController.GetById : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sql = "Select * from UserId";
                var parameters = new DynamicParameters();
                var result = await _genericRepository.QueryAsync<UserId>(sql, parameters, CommandType.Text);




                if (!result.Any())
                {
                    return NotFound("No records found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location UserIdController.GetAll : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserId userid)
        {
            try
            {
                if (userid == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.sp_InsertUserId";
                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", userid.FirstName, DbType.String);
                parameters.Add("@LastName", userid.LastName, DbType.String);
                parameters.Add("@Address ", userid.Address, DbType.String);
                parameters.Add("@Phone", userid.Phone, DbType.String);
                parameters.Add("@Email", userid.Email, DbType.String);
                parameters.Add("@UserName", userid.UserName, DbType.String);
                parameters.Add("@CurrentPassword", userid.CurrentPassword, DbType.String);
                parameters.Add("@NewPassword ", userid.NewPassword, DbType.String);


                await _genericRepository.QueryAsync<UserId>(sql, parameters, CommandType.StoredProcedure);

                _logger.LogInformation("New UserID Created : " + userid.FirstName);

                return Ok("Entity created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location UserIdController.Create : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserId userid)
        {
            try
            {
                if (userid == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.spUpdateUserId";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int64);
                parameters.Add("@FirstName", userid.FirstName, DbType.String);
                parameters.Add("@LastName", userid.LastName, DbType.String);
                parameters.Add("@Address ", userid.Address, DbType.String);
                parameters.Add("@Phone", userid.Phone, DbType.String);
                parameters.Add("@Email", userid.Email, DbType.String);
                parameters.Add("@UserName", userid.UserName, DbType.String);
                parameters.Add("@CurrentPassword", userid.CurrentPassword, DbType.String);
                parameters.Add("@NewPassword ", userid.NewPassword, DbType.String);

                await _genericRepository.QueryAsync<UserId>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location UserIdController.Update : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sql = "dbo.spDeleteUserId";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);



                await _genericRepository.QueryAsync<UserId>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location UserIdController.Delete : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}

