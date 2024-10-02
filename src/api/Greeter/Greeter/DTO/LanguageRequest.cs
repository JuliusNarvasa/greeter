namespace Greeter.DTO;

public class LanguageRequestDto
{
    public LanguageRequest[] languages { get; set; }
}

public class LanguageRequest
{
    public string Language { get; set; }
    public string CountryOfOrigin { get; set; }
}