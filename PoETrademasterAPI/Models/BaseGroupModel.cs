namespace PoETrademasterAPI.Models
{
    public class BaseGroupModel
    {
        public int BaseGroupId { get; set; }

        public string BaseGroupName { get; set; } = null!;

        public int MaxAffixes { get; set; }

        public bool CanBeRare { get; set; }

        public bool CanBeInfluenced { get; set; }

        public bool CanUseFossil { get; set; }

        public bool CanUseEssence { get; set; }

        public bool AllowsCraftedAffix { get; set; }

        public bool IsNotable { get; set; }

        public bool CanUseCatalyst { get; set; }
    }
}
