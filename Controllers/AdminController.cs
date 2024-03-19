using bfw_WebApp_PersonenDB_2024_03_14.DAL;
using bfw_WebApp_PersonenDB_2024_03_14.Models;
using Microsoft.AspNetCore.Mvc;

namespace bfw_WebApp_PersonenDB_2024_03_14.Controllers;

public class AdminController : Controller
{
    
    private readonly IAccessible _dal;
    private readonly IWebHostEnvironment _hostInfo;
    
    public AdminController(IConfiguration configuration, IWebHostEnvironment hostInfo)
    {
        string conn = configuration.GetConnectionString("DefaultConnection")!;
        _dal = new SqlDal(conn);
        
        _hostInfo = hostInfo;
        
    }
    
    // GET
    public IActionResult Index()
    {
        List<Person> personList = new();
        _dal.GetAllPersons().ForEach(p => personList.Add(p));
        return View(personList);
        
    } 
    
    [HttpGet]
    public IActionResult Create(Person person)
    {
        return View();
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create(UploadModel uploadModel)
    {
        
        ModelState.Remove("Uploaddatei");
        
        if (!ModelState.IsValid)
        {
            return View(uploadModel);
        }
        
        if (uploadModel.Uploaddatei == null ||
            uploadModel.Uploaddatei.Length == 0 ||
            !uploadModel.Uploaddatei.ContentType.Contains("image"))
        {
            
            ModelState.AddModelError("Uploaddatei", "Es wurde keine Datei hochgeladen oder die Datei ist kein Bild.");
            return View(uploadModel);
        }
        
        // Upload-Datei auf Datenträger (ins wwwroot) des Webservers speichern

        string pfad = _hostInfo.WebRootPath + "\\uploads";
        string guid = Guid.NewGuid().ToString();
        string extension = Path.GetExtension(uploadModel.Uploaddatei.FileName);
        string neuerName = guid + extension;

        using (var stream = new FileStream(pfad + "\\" + neuerName, FileMode.Create))
        {
            await uploadModel.Uploaddatei.CopyToAsync(stream);
        }
        
        uploadModel.PersonModel.Bild = neuerName;
            
        int pid = _dal.AddPerson(uploadModel.PersonModel);
        
        return RedirectToAction("Details", "Home", new { pid = pid });
    }
}