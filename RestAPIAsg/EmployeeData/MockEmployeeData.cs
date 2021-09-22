
using Microsoft.AspNetCore.JsonPatch;
using RestAPIAsg.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestAPIAsg.EmployeeData
{
    public class MockEmployeeData : IEmployeeData
    {
        public List<Employee> employees = new List<Employee>()
        {
            new Employee() {Id=Guid.NewGuid(),Name="Wipro One",Gender="M",employeeId="ta12"},
            new Employee() {Id=Guid.NewGuid(),Name="Wipro two",Gender="F",employeeId="tb12"},
            new Employee() {Id=Guid.NewGuid(),Name="Wipro three",Gender="M",employeeId="tc12"},
            new Employee() {Id=Guid.NewGuid(),Name="Wipro four",Gender="F",employeeId="td12"},
            new Employee() {Id=Guid.NewGuid(),Name="Wipro five",Gender="M",employeeId="te12"},
            new Employee() {Id=Guid.NewGuid(),Name="Wipro six",Gender="F",employeeId="tf12"},
            new Employee() {Id=Guid.NewGuid(),Name="Wipro seven",Gender="M",employeeId="tg12"},
        };
        
        public Employee AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            employees.Add(employee);
            return employee;
        }

        public void DeleteEmployee(Guid id)
        {
            var emp = employees.Where(x => x.Id == id);
            employees.Remove((Employee)emp);
        }

        public Employee EditEmployee(Employee employee)
        {
            var existingEmployee = GetEmployee(employee.Id);
            existingEmployee.Name = employee.Name;
            existingEmployee.employeeId = employee.employeeId;
            existingEmployee.Gender = employee.Gender;
            return existingEmployee;
        }

        public Employee GetEmployee(Guid id)
        {
            return employees.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }


        public Employee UpdateEmployeeByPatch(Guid Id, JsonPatchDocument<Employee> EmpModel)
        {
            var EmpDetails = GetEmployee(Id);

            if (EmpDetails != null)
            {
                EmpModel.ApplyTo(EmpDetails);
            }
            return EmpDetails;
        }

        //public Employee PatchEmployee(Guid id, Employee employee)
        //{

        //}
        //{
        //    var existingEmployee = employees.SingleOrDefault(x => x.Id == id);
        //    existingEmployee.Id = employee.Id;
        //    existingEmployee.Name = employee.Name;

        //    return existingEmployee;
        //}
    }
}
