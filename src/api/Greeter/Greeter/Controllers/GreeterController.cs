using Greeter.DTO;
using Greeter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greeter.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class GreeterController(LanguageService languageService) : ControllerBase
{
    [HttpGet("get-languages")]
    public async Task<IActionResult> GetLanguages()
    {
        try
        {
            var languages = await languageService.GetLanguages();
            return Ok(languages);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }

    [HttpPost("add-languages")]
    public async Task<IActionResult> AddLanguages([FromBody] LanguageRequestDto request)
    {
        var languages = await languageService.AddLanguages(request);
        return Ok(languages);
    }
    
    [HttpPost("get-greeting")]
    public async Task<IActionResult> GetGreeting([FromForm] string name)
    {
        var greeting = await languageService.GetRandomLanguageGreeting(name);
        return Ok(greeting);
    }
}