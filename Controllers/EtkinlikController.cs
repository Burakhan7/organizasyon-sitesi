using Microsoft.AspNetCore.Mvc;
using OrganizasyonSitesi.Services;

namespace OrganizasyonSitesi.Controllers;

public class EtkinlikController : Controller
{
    private readonly IEtkinlikService _etkinlikService;

    public EtkinlikController(IEtkinlikService etkinlikService)
    {
        _etkinlikService = etkinlikService;
    }

    [Route("yaptigimiz-isler")]
    public async Task<IActionResult> Index()
    {
        var etkinlikler = await _etkinlikService.YayindakileriGetirAsync();
        return View(etkinlikler);
    }

    [Route("etkinlik/{slug}")]
    public async Task<IActionResult> Detay(string slug)
    {
        var etkinlik = await _etkinlikService.SlugIleGetirAsync(slug);
        if (etkinlik == null) return NotFound();   // taslak veya yok → 404 sayfamız devrede

        return View(etkinlik);
    }
}