using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
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
        employeeFactoryMock.Setup(m => m.CreateEmployee("Kevin", It.IsAny<string>(), null, false))
            .Returns(new InternalEmployee("Kevin", "Dockx", 5, 2500, false, 1));
        employeeFactoryMock.Setup(m => m.CreateEmployee("Sandy", It.IsAny<string>(), null, false))
            .Returns(new InternalEmployee("Sandy", "Dockx", 0, 3000, false, 1));
        employeeFactoryMock.Setup(m =>
            m.CreateEmployee(It.Is<string>(value => value.Contains("a")), It.IsAny<string>(), null, false))
            .Returns(new InternalEmployee("SomeoneWithAna", "Dockx", 0, 3000, false, 1));

        var employeeService = new EmployeeService(employeeManagementTestDataRepository, employeeFactoryMock.Object);

        decimal suggestedBonus = 1000;
        var employee = employeeService.CreateInternalEmployee("Kevin", "Dockx");

        Assert.Equal(suggestedBonus, employee.SuggestedBonus);
    }
}
