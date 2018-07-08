using Microsoft.AspNetCore.Mvc;



public class HomeController : Controller
{
    public IActionResult TestIndex()
    {
        var user = new UserModel();
        return View(model: user);
    }
}