using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class BaseAffix
{
    public int BaseAffixId { get; set; }

    public int BaseId { get; set; }

    public int AffixId { get; set; }

    public virtual Affix Affix { get; set; } = null!;

    public virtual Base Base { get; set; } = null!;

    public virtual ICollection<BaseAffixTier> BaseAffixTiers { get; set; } = new List<BaseAffixTier>();
}
