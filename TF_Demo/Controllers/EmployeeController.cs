using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TF_Demo.Data;
using TF_Demo.Models;

namespace TF_Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly MyAppDbContext _context;

        public EmployeeController(MyAppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var Employees = _context.Employees.ToList();
            if (Employees.Count == 0)
            {
                return NotFound("not available");
            }
            return Ok(Employees);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Employee = _context.Employees.Find(id);
            if (Employee == null)
            {
                return NotFound($"not found {id}");
            }
            return Ok(Employee);
        }
        [HttpPost]
        public IActionResult Post(Tf_Employee model)
        {
            _context.Add(model);
            _context.SaveChanges();
            return Ok("employee created");
        }
        [HttpPut]
        public IActionResult Put(Tf_Employee model)
        {
            if (model == null || model.Id == 0)
            {
                if (model == null)
                {
                    return BadRequest("model data is invalid");
                }
                else if (model.Id == 0)
                {
                    return BadRequest($"Employee id {model.Id} is invalid");
                }
            }
            var Employee = _context.Employees.Find(model.Id);
            if (Employee == null)
            {
                return NotFound($"Employee not found with id {model.Id}");
            }

            Employee.EmployeeName = model.EmployeeName;
            Employee.Email = model.Email;
            Employee.PhoneNumber = model.PhoneNumber;
            _context.SaveChanges();
            return Ok("Employee details are updated");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Employee = _context.Employees.Find(id);
            if (Employee == null)
            {
                return NotFound("not found");
            }
            _context.Employees.Remove(Employee);
            _context.SaveChanges();
            return Ok("Employee details are deleted");
        }



    }
}
