namespace PoETrademasterAPI.Models
{
    public class ItemPropertyModel
    {
        public int ItemPropertyId { get; set; }
        public string Property { get; set; }
        public int ItemId { get; set; }
        public int ItemPropertyTypeId { get; set; }
        public int OrderNum { get; set; }
    }
}
