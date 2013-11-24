/*
Prototype pattern is used to create a duplicate object or clone of the current object to enhance performance. 
This pattern is used when creation of objects is costly or complex.
For Example: An object is to be created after a costly database operation. 
We can cache the object, returns its clone on next request and update the database as and when needed thus reducing database calls.
*/


/// <summary>
/// The 'Prototype' interface
/// </summary>

public interface IEmployee
{
	IEmployee Clone();
	string GetDetails();
}

/// <summary>
/// A 'ConcretePrototype' class
/// </summary>

public class Developer : IEmployee
{
	public int WordsPerMinute { get; set; }
	public string Name { get; set; }
	public string Role { get; set; }
	public string PreferredLanguage { get; set; }

	public IEmployee Clone()
	{
		// Shallow Copy: only top-level objects are duplicated
		return (IEmployee)MemberwiseClone();

		// Deep Copy: all objects are duplicated
		//return (IEmployee)this.Clone();
	}

	public string GetDetails()
	{
		return string.Format("{0} - {1} - {2}", Name, Role, PreferredLanguage);
	}
}

/// <summary>
/// A 'ConcretePrototype' class
/// </summary>

public class Typist : IEmployee
{
	public int WordsPerMinute { get; set; }
	public string Name { get; set; }
	public string Role { get; set; }

	public IEmployee Clone()
	{
		// Shallow Copy: only top-level objects are duplicated
		return (IEmployee)MemberwiseClone();

		// Deep Copy: all objects are duplicated
		//return (IEmployee)this.Clone();
	}

	public string GetDetails()
	{
		return string.Format("{0} - {1} - {2}wpm", Name, Role, WordsPerMinute);
	}
}

/// <summary>
/// Prototype Pattern Demo
/// </summary>

class Program
{
	static void Main(string[] args)
	{
		Developer dev = new Developer();
		dev.Name = "Rahul";
		dev.Role = "Team Leader";
		dev.PreferredLanguage = "C#";

		Developer devCopy = (Developer)dev.Clone();
		devCopy.Name = "Arif"; //Not mention Role and PreferredLanguage, it will copy above

		Console.WriteLine(dev.GetDetails());
		Console.WriteLine(devCopy.GetDetails());

		Typist typist = new Typist();
		typist.Name = "Monu";
		typist.Role = "Typist";
		typist.WordsPerMinute = 120;

		Typist typistCopy = (Typist)typist.Clone();
		typistCopy.Name = "Sahil";
		typistCopy.WordsPerMinute = 115;//Not mention Role, it will copy above

		Console.WriteLine(typist.GetDetails());
		Console.WriteLine(typistCopy.GetDetails());

		Console.ReadKey();

	}
}

