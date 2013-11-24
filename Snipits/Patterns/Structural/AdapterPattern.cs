/// <summary>
/// The 'Client' class
/// </summary>
public class ThirdPartyBillingSystem
{
	private ITarget employeeSource;

	public ThirdPartyBillingSystem(ITarget employeeSource)
	{
	this.employeeSource = employeeSource;
	}

	public void ShowEmployeeList()
	{
		List<string> employee = employeeSource.GetEmployeeList();
		//To DO: Implement you business logic

		Console.WriteLine("######### Employee List ##########");
		foreach (var item in employee)
		{
			Console.Write(item); 
		}

	}
}

/// <summary>
/// The 'ITarget' interface
/// </summary>
public interface ITarget
{
	List<string> GetEmployeeList();
}

/// <summary>
/// The 'Adaptee' class
/// </summary>
public class HRSystem
{
	public string[][] GetEmployees()
	{
		string[][] employees = new string[4][];

		employees[0] = new string[] { "100", "Deepak", "Team Leader" };
		employees[1] = new string[] { "101", "Rohit", "Developer" };
		employees[2] = new string[] { "102", "Gautam", "Developer" };
		employees[3] = new string[] { "103", "Dev", "Tester" };

		return employees;
	}
}

/// <summary>
/// The 'Adapter' class
/// </summary>
public class EmployeeAdapter : HRSystem, ITarget
{
	public List<string> GetEmployeeList()
	{
		List<string> employeeList = new List<string>();
		string[][] employees = GetEmployees();
		foreach (string[] employee in employees)
		{
			employeeList.Add(employee[0]);
			employeeList.Add(",");
			employeeList.Add(employee[1]);
			employeeList.Add(",");
			employeeList.Add(employee[2]);
			employeeList.Add("\n");
		}

		return employeeList;
	}
}

/// 
/// Adapter Design Pattern Demo
/// 
class Program
{
	static void Main(string[] args)
	{
		ITarget Itarget = new EmployeeAdapter();
		ThirdPartyBillingSystem client = new ThirdPartyBillingSystem(Itarget);
		client.ShowEmployeeList();

		Console.ReadKey();

	}
}