using Microsoft.AspNetCore.Mvc;
using OrganizasyonSitesi.Services;

namespace OrganizasyonSitesi.Controllers;

public class HizmetController : Controller
{
    private readonly IHizmetService _hizmetService;

    public HizmetController(IHizmetService hizmetService)
    {
        _hizmetService = hizmetService;
    }

    [Route("hizmet/{slug}")]
    public async Task<IActionResult> Detay(string slug)
    {
        var hizmet = await _hizmetService.SlugIleHizmetGetirAsync(slug);
        if (hizmet == null) return NotFound();

        return View(hizmet);
    }
}