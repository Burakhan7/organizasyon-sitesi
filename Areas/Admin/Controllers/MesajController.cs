using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizasyonSitesi.Services;

namespace OrganizasyonSitesi.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class MesajController : Controller
{
    private readonly IHizmetService _hizmetService;

    public MesajController(IHizmetService hizmetService)
    {
        _hizmetService = hizmetService;
    }

    public async Task<IActionResult> Index()
    {
        var mesajlar = await _hizmetService.MesajlariGetirAsync();
        return View(mesajlar);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OkunduIsaretle(int id, bool okundu)
    {
        await _hizmetService.OkunduIsaretleAsync(id, okundu);
        return RedirectToAction(nameof(Index));
    }
}