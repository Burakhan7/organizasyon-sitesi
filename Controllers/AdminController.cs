using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrganizasyonSitesi.Models.ViewModels;

namespace OrganizasyonSitesi.Controllers;

public class AdminController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AdminController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Giris(string? returnUrl = null)
    {
        // Zaten girişliyse tekrar login gösterme
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction(nameof(Panel));

        ViewData["ReturnUrl"] = returnUrl;
        return View(new GirisViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Giris(GirisViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var sonuc = await _signInManager.PasswordSignInAsync(
            model.Email, model.Sifre, model.BeniHatirla, lockoutOnFailure: true);

        if (sonuc.Succeeded)
        {
            // Open redirect saldırısına karşı: sadece kendi sitemize yönlendir
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction(nameof(Panel));
        }

        if (sonuc.IsLockedOut)
        {
            ModelState.AddModelError("", "Çok fazla hatalı deneme. Hesap 10 dakika kilitlendi.");
            return View(model);
        }

        ModelState.AddModelError("", "E-posta veya şifre hatalı.");
        return View(model);
    }


    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cikis()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public IActionResult Panel()
    {
        return RedirectToAction("Index", "Panel", new { area = "Admin" });
    }
}