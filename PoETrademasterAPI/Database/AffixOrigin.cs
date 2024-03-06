using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class AffixOrigin
{
    public int AffixOriginId { get; set; }

    public string AffixOriginName { get; set; } = null!;

    public int? AffixLimit { get; set; }

    public bool IsInfluence { get; set; }

    public bool IsEldritch { get; set; }

    public virtual ICollection<Affix> Affixes { get; set; } = new List<Affix>();
}
