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
    public class TeacherController : ControllerBase
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ILogger<TeacherController> _logger;



        public TeacherController(IGenericRepository genericRepository, ILogger<TeacherController> logger)
        {
            _genericRepository = genericRepository;
            _logger = logger;

        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sql = "dbo.GetTeacherById";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var result = await _genericRepository.QueryFirstOrDefaultAsync<Teacher>(sql, parameters, CommandType.StoredProcedure);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location TeacherController.GetById : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sql = "Select * from Teacher";
                var parameters = new DynamicParameters();
                var result = await _genericRepository.QueryAsync<Teacher>(sql, parameters, CommandType.Text);




                if (!result.Any())
                {
                    return NotFound("No records found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location StudentController.GetAll : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Teacher teacher)
        {
            try
            {
                if (teacher == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.sp_InsertTeacher";
                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", teacher.FirstName, DbType.String);
                parameters.Add("@LastName", teacher.LastName, DbType.String);
                parameters.Add("@OfficialMobile", teacher.OfficialMobile, DbType.String);
                parameters.Add("@OfficialEmail", teacher.OfficialEmail, DbType.String);
                parameters.Add("@Course", teacher.Course, DbType.String);
                parameters.Add("@UploadPhoto", teacher.UploadPhoto, DbType.String);


                await _genericRepository.QueryAsync<Teacher>(sql, parameters, CommandType.StoredProcedure);

                _logger.LogInformation("New Teacher Created : " + teacher.FirstName);

                return Ok("Entity created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location StudentController.Create : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Teacher teacher)
        {
            try
            {
                if (teacher == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.spUpdateTeacher";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int64);
                parameters.Add("@FirstName", teacher.FirstName, DbType.String);
                parameters.Add("@LastName", teacher.LastName, DbType.String);
                parameters.Add("@OfficialMobile", teacher.OfficialMobile, DbType.String);
                parameters.Add("@OfficialEmail", teacher.OfficialEmail, DbType.String);
                parameters.Add("@Course", teacher.Course, DbType.String);
                parameters.Add("@UploadPhoto", teacher.UploadPhoto, DbType.String);


                await _genericRepository.QueryAsync<Teacher>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location StudentController.Update : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var sql = "spDeleteTeacher";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);



                await _genericRepository.QueryAsync<Teacher>(sql, parameters, CommandType.StoredProcedure);

                return Ok("Entity deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location StudentController.Delete : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}

