using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrademasterDataLoader.Classes
{
    internal class Base
    {
        public string id_bgroup { get; set; } = string.Empty;
		public string id_base { get; set; } = string.Empty;
        public string name_base { get; set; } = string.Empty;
        public string is_jewellery { get; set; } = string.Empty;
        public string base_type { get; set; } = string.Empty;
        public string has_childs { get; set; } = string.Empty;
        public string master_base { get; set; } = string.Empty;
        public string unique_notable { get; set; } = string.Empty;
        public string enchant { get; set; } = string.Empty;
        public string is_legacy { get; set; } = string.Empty;
    }
}
