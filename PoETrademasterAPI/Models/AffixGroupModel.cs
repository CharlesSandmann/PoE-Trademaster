namespace PoETrademasterAPI.Models
{
    public class AffixGroupModel
    {
        public int AffixGroupId { get; set; }
        public string AffixGroupName { get; set; }
        public string AffixGroupDisplayId { get; set; }
        public int AffixId { get; set; }
        public bool IsPrimary { get; set; }
    }
}
