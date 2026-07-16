using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizasyonSitesi.Models.ViewModels;
using OrganizasyonSitesi.Services;

namespace OrganizasyonSitesi.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ReferansController : Controller
{
    private readonly IHizmetService _hizmetService;

    public ReferansController(IHizmetService hizmetService)
    {
        _hizmetService = hizmetService;
    }
    public async Task<IActionResult> Index()
    {
        var referanslar = await _hizmetService.TumReferanslariGetirAsync();
        return View(referanslar);
    }

    [HttpGet]
    public IActionResult Ekle()
    {
        return View(new ReferansFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ekle(ReferansFormViewModel form)
    {
        if (!ModelState.IsValid)
            return View(form);

        await _hizmetService.ReferansEkleAsync(form);
        TempData["Basarili"] = "Referans eklendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Duzenle(int id)
    {
        var hizmet = await _hizmetService.ReferansGetirAsync(id);
        if (hizmet == null) return NotFound();

        return View(new ReferansFormViewModel
        {
            Id = hizmet.Id,
            MusteriAdi = hizmet.MusteriAdi,
            Yorum = hizmet.Yorum,
            AktifMi = hizmet.AktifMi,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Duzenle(ReferansFormViewModel form)
    {
        if (!ModelState.IsValid)
            return View(form);

        await _hizmetService.ReferansGuncelleAsync(form);
        TempData["Basarili"] = "Referans güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Sil(int id)
    {
        await _hizmetService.ReferansSilAsync(id);
        TempData["Basarili"] = "Referans silindi.";
        return RedirectToAction(nameof(Index));
    }
}