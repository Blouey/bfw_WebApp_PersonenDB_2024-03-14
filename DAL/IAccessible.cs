using bfw_WebApp_PersonenDB_2024_03_14.Models;

namespace bfw_WebApp_PersonenDB_2024_03_14.DAL;

public interface IAccessible
{
    public List<Person> GetAllPersons();
    public Person GetPersonById(int id);
    public int AddPerson(Person person);
    public bool UpdatePerson(Person person);
    public bool DeletePersonById(int id);
}