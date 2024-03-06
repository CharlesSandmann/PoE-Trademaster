using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class AffixRTag
{
    public int AffixRTagId { get; set; }

    public int TagId { get; set; }

    public int AffixId { get; set; }

    public virtual Affix Affix { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
