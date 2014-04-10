//Lets say you have this kind of class:

class User
{
    public int ID;
    public string FirstName;
    public string LastName;
    public int Age;
    public string City;
    public string Country;
}
// And you creatw a generic list where T is User class. You create n number of users. 
// The point is, that you dont want to show all the fields (or properties) in some query, 
// but only firstname + lastname (as one) of a person and a city that person lives. 
// So you use a new keyword in Linq (i will show how to use don notation of Linq):

List<User> users = new List<User>();
users.Add(new User { ID = 1, FirstName = "first 1", LastName = "last 1", Age = 32, City = "City 1", Country = "Country 1" });
users.Add(new User { ID = 2, FirstName = "first 2", LastName = "last 2", Age = 33, City = "City 2", Country = "Country 2" });
users.Add(new User { ID = 3, FirstName = "first 3", LastName = "last 3", Age = 34, City = "City 3", Country = "Country 3" });

var query = users.Select(s => new
{
    FIRST_LAST = string.Format("{0} {1}", s.FirstName, s.LastName),
    LIVING_IN = s.City
}).ToList();
foreach (var person in query)
{
    string name = person.FIRST_LAST;
    string city = person.LIVING_IN;
}
