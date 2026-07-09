using Microsoft.AspNetCore.Mvc;
using OrganizasyonSitesi.Models.ViewModels;
using OrganizasyonSitesi.Services;

namespace OrganizasyonSitesi.Controllers;

public class HomeController : Controller
{
    private readonly IHizmetService _hizmetService;

    public HomeController(IHizmetService hizmetService)
    {
        _hizmetService = hizmetService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new AnasayfaViewModel
        {
            Hizmetler = await _hizmetService.AktifHizmetleriGetirAsync(),
            Referanslar = await _hizmetService.AktifReferanslariGetirAsync()
        };

        return View(model);
    }

    public IActionResult Privacy() => View();
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Hata()
    {
        return View();
    }

    public IActionResult BulunamayanSayfa()
    {
        return View();
    }
}