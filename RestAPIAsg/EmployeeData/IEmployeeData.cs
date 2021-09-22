using Microsoft.AspNetCore.JsonPatch;
using RestAPIAsg.Models;
using System;
using System.Collections.Generic;

namespace RestAPIAsg.EmployeeData
{
    public interface IEmployeeData
    {
        //List of employees returned
        List<Employee> GetEmployees();
        //Get the single employee by it's id
        Employee GetEmployee(Guid Id);

        //Add an employee, will return the created employee
        Employee AddEmployee(Employee employee);

        //Delete an employee
        void DeleteEmployee(Guid id);

        //Edit an employee
        Employee EditEmployee(Employee employee);
        Employee UpdateEmployeeByPatch(Guid id, JsonPatchDocument<Employee> patchDoc);
    }
}
