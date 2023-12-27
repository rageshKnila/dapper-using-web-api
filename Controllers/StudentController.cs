using Academy.Model;
using Academy.Repository;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using System.Data;

namespace Academy.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ILogger<StudentController> _logger;



        public StudentController(IGenericRepository genericRepository, ILogger<StudentController> logger)
        {
            _genericRepository = genericRepository;
            _logger = logger;

        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var sql = "dbo.GetStudentById";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                var result = await _genericRepository.QueryFirstOrDefaultAsync<Student>(sql, parameters, CommandType.StoredProcedure);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location StudentController.GetById : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sql = "Select * from Student";
                var parameters = new DynamicParameters();
                var result = await _genericRepository.QueryAsync<Student>(sql, parameters, CommandType.Text);



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
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.spInsertstudent";
                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", student.FirstName, DbType.String);
                parameters.Add("@LastName", student.LastName, DbType.String);
                parameters.Add("@Course", student.Course, DbType.String);
                parameters.Add("@BatchDetails", student.BatchDetails, DbType.String);
                parameters.Add("@Gender", student.Gender, DbType.String);
                parameters.Add("@DateOfBirth", student.DateOfBirth, DbType.DateTime);
                parameters.Add("@AdmissionNo", student.AdmissionNo, DbType.String);
                parameters.Add("@BatchTime", student.BatchTime, DbType.String);
                parameters.Add("@Assignedtrainers", student.Assignedtrainers, DbType.String);
                parameters.Add("@Email", student.Email, DbType.String);
                parameters.Add("@PrimePhoneNumbers", student.PrimePhoneNumbers, DbType.String);
                parameters.Add("@UploadPhoto", student.UploadPhoto, DbType.String);


                await _genericRepository.QueryAsync<Student>(sql, parameters, CommandType.StoredProcedure);

                _logger.LogInformation("New Student Created : " + student.FirstName);

                return Ok("Entity created successfully");
            }

            catch (Exception ex)
            {
                _logger.LogError("Exception thrown at location StudentController.Create : " + ex.Message);
                throw new Exception(ex.Message);
            }

        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student student)
        {
            try
            {
                if (student == null)
                {
                    return BadRequest("Invalid model");
                }

                var sql = "dbo.spUpdatestudent";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int64);
                parameters.Add("@FirstName", student.FirstName, DbType.String);
                parameters.Add("@LastName", student.LastName, DbType.String);
                parameters.Add("@Course", student.Course, DbType.String);
                parameters.Add("@BatchDetails", student.BatchDetails, DbType.String);
                parameters.Add("@Gender", student.Gender, DbType.String);
                parameters.Add("@DateOfBirth", student.DateOfBirth, DbType.DateTime);
                parameters.Add("@AdmissionNo", student.AdmissionNo, DbType.String);
                parameters.Add("@BatchTime", student.BatchTime, DbType.String);
                parameters.Add("@Assignedtrainers", student.Assignedtrainers, DbType.String);
                parameters.Add("@Email", student.Email, DbType.String);
                parameters.Add("@PrimePhoneNumbers", student.PrimePhoneNumbers, DbType.String);
                parameters.Add("@UploadPhoto", student.UploadPhoto, DbType.String);


                await _genericRepository.QueryAsync<Student>(sql, parameters, CommandType.StoredProcedure);

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
                var sql = "spDeleteStudent";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id);



                await _genericRepository.QueryAsync<Student>(sql, parameters, CommandType.StoredProcedure);

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

