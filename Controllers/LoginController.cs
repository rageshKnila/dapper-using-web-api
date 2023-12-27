using Academy.Model;
using Academy.Repository;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Academy.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private IConfiguration _config;
		private IGenericRepository _genericRepository;

		public LoginController(IConfiguration configuration, IGenericRepository genericRepository)
		{
			_config = configuration;
			_genericRepository = genericRepository;
		}
        private async Task<IActionResult> AuthenticateUser([FromBody] Login login)
        {

            if (login == null)
            {
                return BadRequest("Invalid model");
            }

            var sql = "dbo.sp_login";
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", login.UserName, DbType.String);
            parameters.Add("@Password", login.Password, DbType.String);

            await _genericRepository.QueryAsync<Users>(sql, parameters, CommandType.StoredProcedure);
            return Ok("Entity created successfully");
        }

        private string GenerateToken(Login login)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], null,
				expires: DateTime.Now.AddMinutes(1),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Login(Login inputUser)
		{
			IActionResult response = Unauthorized();
			var user = AuthenticateUser(inputUser);
			if (user != null)
			{
				var token = GenerateToken(inputUser);
				response = Ok(new { token });
			}

			return response;

		}


	
    }

}

