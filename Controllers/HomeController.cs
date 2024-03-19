using bfw_WebApp_PersonenDB_2024_03_14.Models;
using bfw_WebApp_PersonenDB_2024_03_14.DAL;
using Microsoft.AspNetCore.Mvc;

namespace bfw_WebApp_PersonenDB_2024_03_14.Controllers;

public class HomeController : Controller
{
    private readonly IAccessible _dal;
    
    public HomeController(IConfiguration configuration)
    {
        string conn = configuration.GetConnectionString("DefaultConnection")!;
        _dal = new SqlDal(conn);
    }
    
    
    // GET
    public IActionResult Index()
    {
        List<Person> personList = new();
       _dal.GetAllPersons().ForEach(p => personList.Add(p));
        
        return View(personList);
        
    }
    
    public IActionResult Details(int pid)
    {
        Person person = _dal.GetPersonById(pid);
        return View(person);
    }
    
}