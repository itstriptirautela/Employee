using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly employeemanagementContext Context;
        public EmployeeController(employeemanagementContext EmployeemanagementContext)
        {
            Context = EmployeemanagementContext;

        }

        [HttpPost("add_employee")]
        public IActionResult AddEmployee([FromBody] EmployeeModel employeeobj) //userobj coming from ui
        {
            if (employeeobj == null)
            {
                return BadRequest();
            }
            else
            {
                Context.EmployeeModels.Add(employeeobj);
                Context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Employee Added Successfully"
                });
            }
        }
        [HttpPut("update_employee")]
        public IActionResult UpdatdeEmployee([FromBody] EmployeeModel employeeobj) //userobj coming from ui
        {
            if (employeeobj == null)
            {
                return BadRequest();
            }
            var user = Context.EmployeeModels.AsNoTracking().FirstOrDefault(x => x.Id == employeeobj.Id);
            if (user == null)
            {
                return NotFound(new {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                //whatever field modify it will take and  update entry
                Context.Entry(employeeobj).State = EntityState.Modified;
                Context.SaveChanges();
                return Ok(new
                {
                    statusCode = 200,
                    Message = " Employee Data Updated Sucessfully"
                });

            }
        }
        [HttpDelete("delete_employee/{id}")]// delete through id
        public IActionResult DeleteEmployee(int id)
        {
            var user = Context.EmployeeModels.Find(id);
            if (user == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                Context.Remove(user);
                Context.SaveChanges();
                return Ok(new
                {
                    status = 200,
                    Message = "EmployeeAPI Deleted"
                });
            }
        }
        [HttpGet("get_all_employee")]
        public IActionResult GetAllEmployees()
        { 
            var employee = Context.EmployeeModels.AsQueryable();
            return Ok(new
            {
                SatusCode = 200,
                EmployeeDetails = employee

            });


        }
        [HttpGet("get_employee/id")]
        public IActionResult Getemployee(int id)
        {
            var employee = Context.EmployeeModels.Find(id);
            if(employee == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                return Ok(new
                {
                    Status = 200,
                    EmployeeDetails = employee
                }) ;
            }
        }
            }
}


