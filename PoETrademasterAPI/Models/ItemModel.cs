namespace PoETrademasterAPI.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int BaseId { get; set; }
        public string ImgLocation { get; set; }
        public bool IsExperimentedBase { get; set; }
        public List<ItemPropertyModel> ItemProperties { get; set; }   
    }
}