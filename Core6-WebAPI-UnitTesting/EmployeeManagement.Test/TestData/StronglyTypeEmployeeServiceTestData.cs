namespace EmployeeManagement.Test.TestData;
public class StronglyTypeEmployeeServiceTestData : TheoryData<int, bool>
{
	public StronglyTypeEmployeeServiceTestData()
	{
		Add(100, true);
		Add(200, false);
	}
}
