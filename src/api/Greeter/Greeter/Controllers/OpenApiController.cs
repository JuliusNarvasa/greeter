using Greeter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greeter.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class OpenApiController(OpenApiChatService openApiChatService) : ControllerBase
{
    [HttpGet("test-api")]
    public async Task<IActionResult> GetLanguages()
    {
        try
        {
            var languages = await openApiChatService.RequestGreetingTranslation("Test");
            return Ok(languages);
        }
        catch (ApplicationException e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}