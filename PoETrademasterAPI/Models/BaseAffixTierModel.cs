namespace PoETrademasterAPI.Models
{
    public class BaseAffixTierModel
    {
        public int BaseAffixTierId { get; set; }
        public int BaseAffixId { get; set; }
        public decimal? Stat1StartValue { get; set; } 
        public decimal? Stat1EndValue { get; set; }
        public decimal? Stat2StartValue { get; set; }
        public decimal? Stat2EndValue { get; set; }
        public decimal? Stat3StartValue { get; set; }
        public decimal? Stat3EndValue { get; set; }
        public decimal? Stat4StartValue { get; set; }
        public decimal? Stat4EndValue { get; set; }
        public bool IsElevated { get; set; }
        public int ILvlRequirement { get; set; }
        public int Weight { get; set; }
    }
}
