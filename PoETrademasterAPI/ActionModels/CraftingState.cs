namespace PoETrademasterAPI.ActionModels
{
    public class CraftingState
    {
        public ItemRarity Rarity { get; set; }
        public List<int> RequiredAffixIds { get; set; }
        public List<int> FracturedRequiredAffixIds { get; set; }
        public int PrefixCount { get; set; }
        public int SuffixCount { get; set; }
        public bool IsSynth { get; set; }
        public List<int> Influences { get; set; }
    }
}