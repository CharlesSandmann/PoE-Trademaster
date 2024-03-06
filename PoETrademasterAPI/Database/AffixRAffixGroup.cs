using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class AffixRAffixGroup
{
    public int AffixRAffixGroupId { get; set; }

    public int AffixId { get; set; }

    public int AffixGroupId { get; set; }

    public bool IsPrimary { get; set; }

    public virtual Affix Affix { get; set; } = null!;

    public virtual AffixGroup AffixGroup { get; set; } = null!;
}
