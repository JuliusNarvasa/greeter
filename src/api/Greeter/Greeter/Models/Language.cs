using System;
using System.Collections.Generic;

namespace Greeter.Models;

public partial class Language
{
    public Guid LanguageId { get; set; }

    public string Language1 { get; set; } = null!;

    public virtual ICollection<Translation> Translations { get; set; } = new List<Translation>();
}
