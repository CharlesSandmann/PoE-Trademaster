using System;
using System.Collections.Generic;

namespace PoETrademasterAPI.Database;

public partial class ItemProperty
{
    public int ItemPropertyId { get; set; }

    public string Property { get; set; } = null!;

    public int ItemId { get; set; }

    public int ItemPropertyTypeId { get; set; }

    public int OrderNum { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual ItemPropertyType ItemPropertyType { get; set; } = null!;
}
