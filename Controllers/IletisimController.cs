using Microsoft.AspNetCore.Mvc;
using OrganizasyonSitesi.Models.Entities;
using OrganizasyonSitesi.Models.ViewModels;
using OrganizasyonSitesi.Services;

namespace OrganizasyonSitesi.Controllers;

public class IletisimController : Controller
{
	private readonly IHizmetService _hizmetService;

	public IletisimController(IHizmetService hizmetService)
	{
		_hizmetService = hizmetService;
	}

	[HttpGet]
	public IActionResult Index()
	{
		return View(new IletisimFormViewModel());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Index(IletisimFormViewModel form)
	{
		if (!ModelState.IsValid)
		{
			return View(form);
		}
		 
		var mesaj = new IletisimMesaji
		{
			AdSoyad = form.AdSoyad,
			Email = form.Email,
			Telefon = form.Telefon,
			EtkinlikTuru = form.EtkinlikTuru,
			Mesaj = form.Mesaj
		};

		await _hizmetService.IletisimMesajiKaydetAsync(mesaj);

		TempData["Basarili"] = "Mesajınız alındı! En kısa sürede size dönüş yapacağız.";
		return RedirectToAction(nameof(Index));
	}
}