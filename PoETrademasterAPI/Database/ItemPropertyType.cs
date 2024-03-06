using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class ItemPropertyType
{
    public int ItemPropertyTypeId { get; set; }

    public string ItemPropertyTypeName { get; set; } = null!;

    public virtual ICollection<ItemProperty> ItemProperties { get; set; } = new List<ItemProperty>();
}
