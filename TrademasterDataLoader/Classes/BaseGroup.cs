using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrademasterDataLoader.Classes
{
    internal class BaseGroup
    {
        public string id_bgroup { get;set; } = string.Empty;
        public string name_bgroup { get; set; } = string.Empty;
        public string max_affix { get; set; } = string.Empty;
        public string is_rare { get; set; } = string.Empty;
        public string is_influenced { get; set; } = string.Empty;
        public string is_fossil { get; set; } = string.Empty;
        public string is_ess { get; set; } = string.Empty;
        public string is_craftable { get; set; } = string.Empty;
        public string is_notable { get; set; } = string.Empty;
        public string is_catalyst { get; set; } = string.Empty;
        public string has_items { get; set; } = string.Empty;
    }
}
