using EmployeeManagement.DataAccess.Entities;

namespace EmployeeManagement.Test;
public class EmployeeTests
{
    [Fact]
    public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameIsConcatenation()
    {
        var employee = new InternalEmployee("Kevin", "Dockx", 0, 2500, false, 1);

        employee.FirstName = "Lucia";
        employee.LastName = "Shelton";

        Assert.Equal("Lucia Shelton", employee.FullName);
    }
}
