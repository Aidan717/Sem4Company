using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
    public class ResponseStatus {
        public string _index { get; set; }
        public string _type { get; set; }
        public string _id { get; set; }
        public int _version { get; set; }
        public string result { get; set; }
        public _Shards _shards { get; set; }
        public int _seq_no { get; set; }
        public int _primary_term { get; set; }

        public ResponseStatus() {
           
        }

        public ResponseStatus(string result) {
            this.result = result;
        }

        public class _Shards {
            public int total { get; set; }
            public int successful { get; set; }
            public int failed { get; set; }
        }

    }
}
