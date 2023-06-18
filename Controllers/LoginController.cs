using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Data;
using EmployeeAPI.Helpers;
//using System.IdentityModel.Tokens.Jwt;
using System.Text;
//using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly employeemanagementContext Context;
        private readonly IConfiguration _config;
        public LoginController(employeemanagementContext EmployeemanagementContext,IConfiguration config)
        {
            Context = EmployeemanagementContext;
            _config = config;
        }
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var userdetails = Context.UserModels.AsQueryable();//get all data  from  database
            return Ok(userdetails);
        }
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] UserModel userobj) //userobj coming from ui
        {
            if (userobj == null)
            {
                return BadRequest();
            }
            else
            {
                userobj.Password = EncDscPassword.EncryptPassword(userobj.Password);
                Context.UserModels.Add(userobj);
                Context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Sign Up Successfully"
                }
                    );

            }
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel userobj) //userobj coming from ui
        {
            if (userobj == null)
            {
                return BadRequest();
            }
            else
            {
                var user = Context.UserModels.Where(a => a.UserName == userobj.UserName).FirstOrDefault();
                if (user != null && EncDscPassword.DecryptPassword(user.Password) == userobj.Password)
                {
                    //var token = GenerateToken(user.UserName);
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Logged In Successfully",
                        UserType = user.UserType,
                        //JwtToken =token
                    }) ;
                }
                else
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "User Not Found"
                    }
                        );

                }


            }
        }

        //private string GenerateToken(string username)
        //{
        //    var tokenhandler = new JwtSecurityTokenHandler();
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
        //    var credential = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Email,username),
        //        new Claim("ComanyName","Lets Program")
        //    };
        //    var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"],
        //        audience: _config["Jwt:Audience"],
        //        claims,
        //        expires: DateTime.Now.AddDays(1),
        //    signingCredentials: credential);
                
        //    return tokenhandler.WriteToken(token);
        //}
    }
}


