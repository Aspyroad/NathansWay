/*
What is Composite Pattern
Composite pattern is used to separate an abstraction from its implementation so that both can be modified independently.
Composite pattern is used when we need to treat a group of objects and a single object in the same way. 
Composite pattern composes objects in term of a tree structure to represent part as well as whole hierarchies.
This pattern creates a class contains group of its own objects. This class provides ways to modify its group of same objects.

Component : This is an abstract class containing members that will be implemented by all object in the hierarchy. 
It acts as the base class for all the objects within the hierarchy
Composite : This is a class which includes Add,Remove,Find and Get methods to do operations on child components.
Leaf : This is a class which is used to define leaf components within the tree structure means these cannot have children.

Great article from stackoverflow
www.stackoverflow.com/questions/49002
Prefer composition over inheritance as it is more malleable / easy to modify later, but do not use a compose-always approach. 
With composition, it's easy to change behavior on the fly with Dependency Injection / Setters. 
Inheritance is more rigid as most languages do not allow you to derive from more than one type.
So the goose is more or less cooked once you derive from Class A.

My acid test for the above is:

	Does TypeB want to expose the complete interface (all public methods no less) of TypeA such that TypeB can be used where TypeA is expected? Indicates Inheritance.
	e.g. A Cessna biplane will expose the complete interface of an airplane, if not more. So that makes it fit to derive from Airplane.

	Does TypeB only want only some/part of the behavior exposed by TypeA? Indicates need for Composition.
	e.g. A Bird may need only the fly behavior of an Airplane. In this case, it makes sense to extract it out as an interface / class / both and make it a member of both classes.


*/

/// <summary>
/// The 'Component' Treenode
/// </summary>
public interface IEmployed
{
	int EmpID { get; set; }
	string Name { get; set; }
}

/// <summary>
/// The 'Composite' class
/// </summary>
public class Employee : IEmployed, IEnumerable<IEmployed>
{
	private List<IEmployed> _subordinates = new List<IEmployed>();

	public int EmpID { get; set; }
	public string Name { get; set; }

	public void AddSubordinate(IEmployed subordinate)
	{
		_subordinates.Add(subordinate);
	}

	public void RemoveSubordinate(IEmployed subordinate)
	{
		_subordinates.Remove(subordinate);
	}

	public IEmployed GetSubordinate(int index)
	{
		return _subordinates[index];
	}

	public IEnumerator<IEmployed> GetEnumerator()
	{
		foreach (IEmployed subordinate in _subordinates)
		{
			yield return subordinate;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

/// <summary>
/// The 'Leaf' class
/// </summary>
public class Contractor : IEmployed
{
	public int EmpID { get; set; }
	public string Name { get; set; }
}

class Program
{
	static void Main(string[] args)
	{
		Employee Rahul = new Employee { EmpID = 1, Name = "Rahul" };
		Employee Amit = new Employee { EmpID = 2, Name = "Amit" };
		Employee Mohan = new Employee { EmpID = 3, Name = "Mohan" };

		Rahul.AddSubordinate(Amit);
		Rahul.AddSubordinate(Mohan);

		Employee Rita = new Employee { EmpID = 4, Name = "Rita" };
		Employee Hari = new Employee { EmpID = 5, Name = "Hari" };

		Amit.AddSubordinate(Rita);
		Amit.AddSubordinate(Hari);

		Employee Kamal = new Employee { EmpID = 6, Name = "Kamal" };
		Employee Raj = new Employee { EmpID = 7, Name = "Raj" };

		Contractor Sam = new Contractor { EmpID = 8, Name = "Sam" };
		Contractor tim = new Contractor { EmpID = 9, Name = "Tim" };

		Mohan.AddSubordinate(Kamal);
		Mohan.AddSubordinate(Raj);
		Mohan.AddSubordinate(Sam);
		Mohan.AddSubordinate(tim);

		Console.WriteLine("EmpID={0}, Name={1}", Rahul.EmpID, Rahul.Name);

		foreach (Employee manager in Rahul)
		{
			Console.WriteLine("\n EmpID={0}, Name={1}", manager.EmpID, manager.Name);

			foreach (var employee in manager)
			{
				Console.WriteLine(" \t EmpID={0}, Name={1}", employee.EmpID, employee.Name);
			}
		}

		Console.ReadKey();
	}
} 



