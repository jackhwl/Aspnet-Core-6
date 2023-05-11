using EmployeeManagement.Business;
using EmployeeManagement.Services.Test;
using Moq;

namespace EmployeeManagement.Test;
public class MoqTests
{
    [Fact]
    public void FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated()
    {
        var employeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
        //var employeeFactory = new EmployeeFactory();
        var employeeFactoryMock = new Mock<EmployeeFactory>();
        var employeeService = new EmployeeService(employeeManagementTestDataRepository, employeeFactoryMock.Object);

        var employee = employeeService.FetchInternalEmployee(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

        Assert.Equal(400, employee.SuggestedBonus);
    }

    [Fact]
    public void CreateInternalEmployee_InternalEmployeeCreated_SuggestedBonusMustBeCalculated()
    {
        // Arrange
        var employeeManagementTestDataRepository =
          new EmployeeManagementTestDataRepository();
        var employeeFactoryMock = new Mock<EmployeeFactory>();
        var employeeService = new EmployeeService(employeeManagementTestDataRepository, employeeFactoryMock.Object);

        decimal suggestedBonus = 200;
        var employee = employeeService.CreateInternalEmployee("Kevin", "Dockx");

        Assert.Equal(suggestedBonus, employee.SuggestedBonus);
    }
}
