using System;
using System.Collections.Generic;

namespace Greeter.Models;

public partial class Translation
{
    public Guid TranslationId { get; set; }

    public Guid LanguageId { get; set; }

    public string Morning { get; set; } = null!;

    public string Afternoon { get; set; } = null!;

    public string Evening { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;
}
