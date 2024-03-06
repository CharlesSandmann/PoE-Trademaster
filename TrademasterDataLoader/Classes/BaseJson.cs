using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrademasterDataLoader.Classes
{
    internal class BaseJson
    {
        public Bitems bitems { get; set; }

        public Bases bases { get; set; }

        public BaseGroups bgroups { get; set; }

        public Modifiers modifiers { get; set; }

        public MGroups mgroups { get; set; }

        public Mtypes mtypes { get; set; }

        public Fossils fossils { get; set; }

        public Catalysts catalysts { get; set; }

        public Essences essences { get; set; }

        public Maevens maeven { get; set; }

        public Aliases aliases { get; set; }

        public Dictionary<string, Dictionary<string, List<Tier>>> tiers { get; set; }

        public Dictionary<string, HashSet<string>> basemods { get; set; }

        public Dictionary<string, HashSet<string>> modbases { get; set; }

        public Dictionary<string, string> mdefs { get; set; }

        // IGNORE THIS
        public List<Clng> clngs { get; set; }
    }
}
