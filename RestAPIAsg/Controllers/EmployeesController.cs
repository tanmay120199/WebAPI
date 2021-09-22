using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestAPIAsg.EmployeeData;
using RestAPIAsg.Models;
using System;

namespace RestAPIAsg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeData _employeeData;
        public EmployeesController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        [HttpGet("getall")]           
        public IActionResult GetEmployees()
        {
          
            return Ok(_employeeData.GetEmployees());

        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound("Employee Not Found");
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Employee emp=_employeeData.AddEmployee(employee);
            return CreatedAtAction("Get", new { id = emp.Id }, emp);
            //to return that the server has created(201) the request, we using Created()
            //return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            if (employee != null)
            {
                _employeeData.DeleteEmployee(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult EditEmployee(Guid id, Employee employee)
        {
            var existingEmployee = _employeeData.GetEmployee(id);
            if (existingEmployee != null)
            {
                employee.Id = existingEmployee.Id;
               Employee emp= _employeeData.EditEmployee(employee);
                return Ok(emp);
            }
       
            return NotFound("Employee Not Found");
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateEmployeeByPatch([FromBody] JsonPatchDocument<Employee> patchDoc,[FromRoute] Guid Id)
        {
            if (patchDoc != null)
            {
                Employee e=_employeeData.UpdateEmployeeByPatch(Id, patchDoc);
                return Ok(GetEmployee(Id));
            }
            else
            {
                return BadRequest("patchDoc object sent is null");
            }
        }
    }
}
