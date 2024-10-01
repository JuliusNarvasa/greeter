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
        try
        {
            var languages = await languageService.GetLanguages("Test");
            return Ok(languages);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}