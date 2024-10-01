using Greeter.Services;
using Microsoft.AspNetCore.Mvc;

namespace Greeter.Controllers;

[ApiController]
[Route("[controller]")]
public class GreeterController(LanguageService languageService) : ControllerBase
{
    [HttpGet("get-languages")]
    public async Task<IActionResult> GetLanguages()
    {
        var languages = await languageService.GetLanguages();
        return Ok(languages);
    }
}