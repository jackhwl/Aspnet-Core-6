using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.Test;
public class DemoInternalEmployeesControllerTests
{
    [Fact]
    public async Task CreateInternalEmployee_InvalidInput_MustReturnBadRequest()
    {
        var employeeServiceMock = new Mock<IEmployeeService>();
        var mapperMock = new Mock<IMapper>();
        var demoInternalEmployeesController = new DemoInternalEmployeesController(employeeServiceMock.Object, mapperMock.Object);

        var internalEmployeeForCreationDto = new InternalEmployeeForCreationDto();
        //{
        //    FirstName = string.Empty, LastName = "Smith"
        //};

        demoInternalEmployeesController.ModelState.AddModelError("FirstName", "Required");

        var result = await demoInternalEmployeesController.CreateInternalEmployee(internalEmployeeForCreationDto);

        var actionResult = Assert.IsType<ActionResult<Models.InternalEmployeeDto>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

        Assert.IsType<SerializableError>(badRequestResult.Value);
    }
}
