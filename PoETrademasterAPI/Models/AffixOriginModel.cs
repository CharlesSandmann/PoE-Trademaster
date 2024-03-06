namespace PoETrademasterAPI.Models
{
    public class AffixOriginModel
    {
        public int AffixOriginId { get; set; }
        public string AffixOriginName { get; set; }
        public int? AffixLimit { get; set; }
        public bool IsInfluence { get; set; }
        public bool IsEldritch { get; set; }
    }
}
