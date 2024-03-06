using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public int BaseId { get; set; }

    public string? ImgLocation { get; set; }

    public bool IsExperimentedBase { get; set; }

    public virtual Base Base { get; set; } = null!;

    public virtual ICollection<ItemProperty> ItemProperties { get; set; } = new List<ItemProperty>();
}
