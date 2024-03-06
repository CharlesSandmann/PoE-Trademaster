using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrademasterDataLoader.Classes
{
    internal class Item
    {
        public string id_bitem { get; set; } = string.Empty;
        public string id_base { get; set; } = string.Empty;
        public string name_bitem { get; set; } = string.Empty;
        public string drop_level { get; set; } = string.Empty;
        public string properties { get; set; } = string.Empty;
        public string requirements { get; set; } = string.Empty;
        public string implicits { get; set; } = string.Empty;
        public string exp { get; set; } = string.Empty;
        public string imgurl { get; set; } = string.Empty;
        public string is_legacy { get; set; } = string.Empty;
        public string exmods { get; set; } = string.Empty;
    }
}
