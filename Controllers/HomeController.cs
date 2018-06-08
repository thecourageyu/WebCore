using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var user = new UserModel();
        return View(model: user);
    }
}