using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizasyonSitesi.Services;

namespace OrganizasyonSitesi.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class PanelController : Controller
{
    private readonly IHizmetService _hizmetService;

    public PanelController(IHizmetService hizmetService)
    {
        _hizmetService = hizmetService;
    }

    public async Task<IActionResult> Index()
    {
        var ozet = await _hizmetService.PanelOzetGetirAsync();
        return View(ozet);
    }
}