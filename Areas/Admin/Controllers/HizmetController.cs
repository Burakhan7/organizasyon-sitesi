using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizasyonSitesi.Models.ViewModels;
using OrganizasyonSitesi.Services;

namespace OrganizasyonSitesi.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class HizmetController : Controller
{
    private readonly IHizmetService _hizmetService;

    public HizmetController(IHizmetService hizmetService)
    {
        _hizmetService = hizmetService;
    }

    public async Task<IActionResult> Index()
    {
        var hizmetler = await _hizmetService.TumHizmetleriGetirAsync();
        return View(hizmetler);
    }

    [HttpGet]
    public IActionResult Ekle()
    {
        return View(new HizmetFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ekle(HizmetFormViewModel form)
    {
        if (!ModelState.IsValid)
            return View(form);

        await _hizmetService.HizmetEkleAsync(form);
        TempData["Basarili"] = "Hizmet eklendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Duzenle(int id)
    {
        var hizmet = await _hizmetService.HizmetGetirAsync(id);
        if (hizmet == null) return NotFound();

        ViewBag.MevcutFotograflar = hizmet.Fotograflar;   // ← doğru yeri burası

        return View(new HizmetFormViewModel
        {
            Id = hizmet.Id,
            Baslik = hizmet.Baslik,
            Aciklama = hizmet.Aciklama,
            DetayAciklama = hizmet.DetayAciklama,
            SiraNo = hizmet.SiraNo,
            AktifMi = hizmet.AktifMi
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Duzenle(HizmetFormViewModel form)
    {
        if (!ModelState.IsValid)
            return View(form);

        await _hizmetService.HizmetGuncelleAsync(form);
        TempData["Basarili"] = "Hizmet güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Sil(int id)
    {
        var (basarili, hata) = await _hizmetService.HizmetSilAsync(id);

        if (basarili)
            TempData["Basarili"] = "Hizmet silindi.";
        else
            TempData["Hatalar"] = hata;

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FotografSil(int id, int hizmetId)
    {
        await _hizmetService.HizmetFotografSilAsync(id);
        TempData["Basarili"] = "Fotoğraf silindi.";
        return RedirectToAction(nameof(Duzenle), new { id = hizmetId });
    }
}