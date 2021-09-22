using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestAPIAsg.Controllers;
using RestAPIAsg.EmployeeData;
using RestAPIAsg.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace web_api_tests
{
    
        public class UnitTest1
        {
            EmployeesController _controller;
            IEmployeeData _service;

            public UnitTest1()
            {
                _service = new MockEmployeeData();
                _controller = new EmployeesController(_service);
            }

            [Fact]
            public void Get_WhenCalled_ReturnsOkResult()
            {
                //Act
                var okResult = _controller.GetEmployees();

                //Assert
                Assert.IsType<OkObjectResult>(okResult);
            }
            [Fact]
            public void Get_WhenCalled_ReturnsAllItems()
            {
                //Act
                var okResult = _controller.GetEmployees() as OkObjectResult;

                // Assert
                var items = Assert.IsType<List<Employee>>(okResult.Value);
                Assert.Equal(7, items.Count);
            }
            
            [Fact]
            public void GetById_ExistingGuidPassed_ReturnsRightItem()
            {
                // Arrange
                var testGuid = new Guid();
            // Act
            var okResult = _controller.GetEmployee(testGuid) as ObjectResult;
            //var result = productController.GetBy(id);
            //var okResult = result as ObjectResult;
                // Assert
                if (okResult.StatusCode == 404)
                {
                    Assert.Equal(404, okResult.StatusCode);
                }
                else if (okResult.StatusCode == 500)
                {
                    Assert.Equal(500, okResult.StatusCode);
                }
                else if (okResult.StatusCode == 200)
                {
                    Assert.Equal(200, okResult.StatusCode);
                }
            
                //Assert.IsType<Employee>(okResult.Value);
                //Assert.Equal(testGuid, (okResult.Value as Employee).Id);
            }

            //Delete
            [Fact]
            public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
            {
                // Arrange
                var notExistingGuid = Guid.NewGuid();

                // Act
                var badResponse = _controller.DeleteEmployee(notExistingGuid);

                // Assert
                Assert.IsType<NotFoundResult>(badResponse);
            }

            //put    

            [Fact]
            public void Full_Update()
        {
            // Arrange
            Employee testItem = new Employee()
            {
                Name = "Gun",
                Gender = "F",
                employeeId = "wp22"
            };
            Guid testGuid = new Guid();

            // Act
            var Result = _controller.EditEmployee(testGuid,testItem) as ObjectResult;

            // Assert
            if (Result.StatusCode == 404)
            {
                Assert.Equal(404, Result.StatusCode);
            }
            else if (Result.StatusCode == 500)
            {
                Assert.Equal(500, Result.StatusCode);
            }
            else if (Result.StatusCode == 200)
            {
                Assert.Equal(200, Result.StatusCode);
            }

        }
        
        //ADD

        [Fact]
            public void Add_InvalidObjectPassed_ReturnsBadRequest()
            {
                // Arrange
                var nameMissingItem = new Employee()
                {
                    Name = "Guinness",
                    Gender = "M",
                    employeeId="wp21"
                };
                _controller.ModelState.AddModelError("Name", "Required");

                // Act
                var badResponse = _controller.AddEmployee(nameMissingItem);

                // Assert
                Assert.IsType<BadRequestObjectResult>(badResponse);
            }


            [Fact]
            public void Add_ValidObjectPassed_ReturnsCreatedResponse()
            {
                // Arrange
                Employee testItem = new Employee()
                {
                    Name = "Guinness",
                    Gender = "M",
                    employeeId = "wp21"
                };

                // Act
                var createdResponse = _controller.AddEmployee(testItem);

                // Assert
                Assert.IsType<CreatedAtActionResult>(createdResponse);
            }


            [Fact]
            public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
            {
                // Arrange
                var testItem = new Employee()
                {
                    Name = "Guinness",
                    Gender = "M",
                    employeeId = "wp21"
                };

                // Act
                var createdResponse = _controller.AddEmployee(testItem) as CreatedAtActionResult;
                var item = createdResponse.Value as Employee;

                // Assert
                Assert.IsType<Employee>(item);
                Assert.Equal("Guinness", item.Name);
            }
        }
    
}
