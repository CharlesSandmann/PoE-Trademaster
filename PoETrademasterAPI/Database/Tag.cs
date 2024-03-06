using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public virtual ICollection<AffixRTag> AffixRTags { get; set; } = new List<AffixRTag>();
}
