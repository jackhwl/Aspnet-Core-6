using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test;

public class EmployeeFactoryTests
{
    [Fact]
    public void CreateEmloyee_ConstructInternalEmployee_SalaryMustBe2500()
    {
        var employeeFactory = new EmployeeFactory();
        var employee = (InternalEmployee) employeeFactory.CreateEmployee("Kevin", "Dockx");
        Assert.Equal(2500, employee.Salary);
    }
}