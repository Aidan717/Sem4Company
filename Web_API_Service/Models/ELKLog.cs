using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
    public class ELKLog {

            public string host { get; set; }
            public string version { get; set; }
            public string http_address { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string ephemeral_id { get; set; }
            public string status { get; set; }
            public bool snapshot { get; set; }
            public Pipeline pipeline { get; set; }
            public DateTime build_date { get; set; }
            public string build_sha { get; set; }
            public bool build_snapshot { get; set; }


        public class Pipeline {
            public int workers { get; set; }
            public int batch_size { get; set; }
            public int batch_delay { get; set; }
        }

    }
}
