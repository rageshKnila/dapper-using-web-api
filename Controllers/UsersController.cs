using Academy.Model;
using Academy.Repository;
using Academy.Service;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;
using System.Data;

namespace Academy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ILogger<UsersController> _logger;



        public UsersController(IGenericRepository genericRepository, ILogger<UsersController> logger)
        {
            _genericRepository = genericRepository;
            _logger = logger;

        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sql = "SELECT * FROM dbo.Users WITH(NOLOCK) Where id=@Id";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var result = await _genericRepository.QueryFirstOrDefaultAsync<Users>(sql, parameters, CommandType.Text);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location UsersController.GetById : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sql = "SELECT * FROM Users";
                var parameters = new DynamicParameters();
                var result = await _genericRepository.QueryAsync<Users>(sql, parameters, CommandType.Text);

                if (!result.Any())
                {
                    return NotFound("No records found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location UsersController.GetAll : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Users users)
        {
            try
            {
                if (users == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.InsertUser";
                var parameters = new DynamicParameters();
                parameters.Add("@Name", users.Name, DbType.String);
                parameters.Add("@Phone", users.Phone, DbType.String);
                parameters.Add("@Email", users.Email, DbType.String);
                parameters.Add("@UserName", users.UserName, DbType.String);
                parameters.Add("@OldPassword", users.OldPassword, DbType.String);
                parameters.Add("@NewPassword", users.NewPassword, DbType.String);
                parameters.Add("@UpdateProfilePicture", users.UpdateProfilePicture, DbType.String);

                await _genericRepository.QueryAsync<Users>(sql, parameters, CommandType.StoredProcedure);

                _logger.LogInformation("New User Created : " + users.Name);

                return Ok("Entity created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location UsersController.Create : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, Users users)
        {
            try
            {
                if (users == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.UpdateUser";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int64);
                parameters.Add("@Name", users.Name, DbType.String);
                parameters.Add("@Phone", users.Phone, DbType.String);
                parameters.Add("@Email", users.Email, DbType.String);
                parameters.Add("@UserName", users.UserName, DbType.String);
                parameters.Add("@OldPassword", users.OldPassword, DbType.String);
                parameters.Add("@NewPassword", users.NewPassword, DbType.String);
                parameters.Add("@UpdateProfilePicture", users.UpdateProfilePicture, DbType.String);


                await _genericRepository.QueryAsync<Users>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location UsersController.Update : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sql = "sp_Delete";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);



                await _genericRepository.QueryAsync<Users>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location UsersController.Delete : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}


