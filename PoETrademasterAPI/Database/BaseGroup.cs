using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class BaseGroup
{
    public int BaseGroupId { get; set; }

    public string BaseGroupName { get; set; } = null!;

    public int MaxAffixes { get; set; }

    public bool CanBeRare { get; set; }

    public bool CanBeInfluenced { get; set; }

    public bool CanUseFossil { get; set; }

    public bool CanUseEssence { get; set; }

    public bool AllowsCraftedAffix { get; set; }

    public bool CanUseCatalyst { get; set; }

    public virtual ICollection<Base> Bases { get; set; } = new List<Base>();
}
