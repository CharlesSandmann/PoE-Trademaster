namespace PoETrademasterAPI.Models
{
    public class BaseModel
    {
        public int BaseId { get; set; }
        public string BaseName { get; set; }
        public int BaseGroupId { get; set; }
        public bool ItemRequired { get; set; }
        public int? ParentBaseId { get; set; }

        public bool IsJewellery => BaseGroupId == 1;
    }
}
