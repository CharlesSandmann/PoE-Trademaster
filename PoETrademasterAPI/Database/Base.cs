using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class Base
{
    public int BaseId { get; set; }

    public string BaseName { get; set; } = null!;

    public int BaseGroupId { get; set; }

    public bool ItemRequired { get; set; }

    public int? ParentBaseId { get; set; }

    public virtual ICollection<BaseAffix> BaseAffixes { get; set; } = new List<BaseAffix>();

    public virtual BaseGroup BaseGroup { get; set; } = null!;

    public virtual ICollection<Base> InverseParentBase { get; set; } = new List<Base>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual Base? ParentBase { get; set; }
}
