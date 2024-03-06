using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class AffixGroup
{
    public int AffixGroupId { get; set; }

    public string? AffixGroupName { get; set; }

    public string AffixGroupDisplayId { get; set; } = null!;

    public virtual ICollection<AffixRAffixGroup> AffixRAffixGroups { get; set; } = new List<AffixRAffixGroup>();
}
