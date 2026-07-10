using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizasyonSitesi.Models.ViewModels;
using OrganizasyonSitesi.Services;

namespace OrganizasyonSitesi.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class EtkinlikController : Controller
{
    private readonly IEtkinlikService _etkinlikService;
    private readonly IHizmetService _hizmetService;

    public EtkinlikController(IEtkinlikService etkinlikService, IHizmetService hizmetService)
    {
        _etkinlikService = etkinlikService;
        _hizmetService = hizmetService;
    }

    public async Task<IActionResult> Index()
    {
        var etkinlikler = await _etkinlikService.TumunuGetirAsync();
        return View(etkinlikler);
    }

    [HttpGet]
    public async Task<IActionResult> Ekle()
    {
        await HizmetListesiniYukleAsync();
        return View(new EtkinlikFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ekle(EtkinlikFormViewModel form)
    {
        if (!ModelState.IsValid)
        {
            await HizmetListesiniYukleAsync();
            return View(form);
        }

        await _etkinlikService.EkleAsync(form);
        TempData["Basarili"] = "Etkinlik eklendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Duzenle(int id)
    {
        var etkinlik = await _etkinlikService.GetirAsync(id);
        if (etkinlik == null) return NotFound();

        await HizmetListesiniYukleAsync();

        return View(new EtkinlikFormViewModel
        {
            Id = etkinlik.Id,
            Baslik = etkinlik.Baslik,
            Aciklama = etkinlik.Aciklama,
            Mekan = etkinlik.Mekan,
            Tarih = etkinlik.Tarih,
            HizmetId = etkinlik.HizmetId,
            YayindaMi = etkinlik.YayindaMi
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Duzenle(EtkinlikFormViewModel form)
    {
        if (!ModelState.IsValid)
        {
            await HizmetListesiniYukleAsync();
            return View(form);
        }

        await _etkinlikService.GuncelleAsync(form);
        TempData["Basarili"] = "Etkinlik güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Sil(int id)
    {
        await _etkinlikService.SilAsync(id);
        TempData["Basarili"] = "Etkinlik silindi.";
        return RedirectToAction(nameof(Index));
    }

    private async Task HizmetListesiniYukleAsync()
    {
        var hizmetler = await _hizmetService.AktifHizmetleriGetirAsync();
        ViewBag.Hizmetler = new SelectList(hizmetler, "Id", "Baslik");
    }
}