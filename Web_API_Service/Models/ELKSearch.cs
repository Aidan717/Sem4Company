using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
    public class ELKSearch {
            public string name { get; set; }
            public string cluster_name { get; set; }
            public string cluster_uuid { get; set; }
            public Version version { get; set; }
            public string tagline { get; set; }

        public class Version {
            public string number { get; set; }
            public string build_flavor { get; set; }
            public string build_type { get; set; }
            public string build_hash { get; set; }
            public DateTime build_date { get; set; }
            public bool build_snapshot { get; set; }
            public string lucene_version { get; set; }
            public string minimum_wire_compatibility_version { get; set; }
            public string minimum_index_compatibility_version { get; set; }
        }

    }
}
