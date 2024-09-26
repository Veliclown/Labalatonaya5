using Microsoft.AspNetCore.Mvc;

public class CookieController : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    
    public IActionResult TestException()
    {
        throw new InvalidOperationException("Помилка");
    }

    [HttpPost]
    public IActionResult Create(string value, DateTime expirationDate)
    {
        if (!string.IsNullOrEmpty(value) && expirationDate > DateTime.Now)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = expirationDate
            };
            Response.Cookies.Append("MyCookie", value, options);
        }
        return RedirectToAction("CheckCookie");
    }

    public IActionResult CheckCookie()
    {
        var cookieValue = Request.Cookies["MyCookie"];
        ViewBag.CookieValue = string.IsNullOrEmpty(cookieValue) ? "Cookies не знайдено" : $"Cookies значення: {cookieValue}";
        return View();
    }
}
