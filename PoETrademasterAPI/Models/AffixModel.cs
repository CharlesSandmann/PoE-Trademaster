namespace PoETrademasterAPI.Models
{
    public class AffixModel
    {
        public int AffixId { get; set; }
        public string Affix { get; set; }
        public string? ElevatedAffix { get; set; }
        public bool IsPrefix { get; set; }
        public bool IsImplicit { get; set; }
        public bool IsNotablePassive { get; set; }
        public bool HasResistance { get; set; }
        public bool HasAttribute { get; set; }
        public int AffixOriginId { get; set; }
        public string? Tags { get; set; }
        public string? AffixGroups { get; set; }
    }
}
