using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class Affix
{
    public int AffixId { get; set; }

    public string AffixName { get; set; } = null!;

    public string? ElevatedAffix { get; set; }

    public bool IsPrefix { get; set; }

    public bool IsImplicit { get; set; }

    public bool IsNotablePassive { get; set; }

    public bool HasResistance { get; set; }

    public bool HasAttribute { get; set; }

    public int AffixOriginId { get; set; }

    public virtual AffixOrigin AffixOrigin { get; set; } = null!;

    public virtual ICollection<AffixRAffixGroup> AffixRAffixGroups { get; set; } = new List<AffixRAffixGroup>();

    public virtual ICollection<AffixRTag> AffixRTags { get; set; } = new List<AffixRTag>();

    public virtual ICollection<BaseAffix> BaseAffixes { get; set; } = new List<BaseAffix>();
    public string? Tags { get; set; }
    public string? AffixGroups { get; set; }
}
