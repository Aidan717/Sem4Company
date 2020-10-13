
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
    public class Schools {

        public int took { get; set; }
        public bool timed_out { get; set; }
        public _Shards _shards { get; set; }
        public Hits hits { get; set; }

        public class _Shards {
            public int total { get; set; }
            public int successful { get; set; }
            public int skipped { get; set; }
            public int failed { get; set; }
        }

        public class Hits {
            public Total total { get; set; }
            public float max_score { get; set; }
            public Hit[] hits { get; set; }
        }

        public class Total {
            public int value { get; set; }
            public string relation { get; set; }
        }

        public class Hit {
            public string _index { get; set; }
            public string _type { get; set; }
            public string _id { get; set; }
            public float _score { get; set; }
            public _Source _source { get; set; }
        }

        public class _Source {
            [Required]
            public string name { get; set; }
            [Required]
            public string description { get; set; }
            [Required]
            public string street { get; set; }
            [Required]
            public string city { get; set; }
            [Required]
            public string state { get; set; }
            [Required]
            public string zip { get; set; }
            [Required]
            public float[] location { get; set; }
            [Required]
            public int fees { get; set; }
            [Required]
            public string[] tags { get; set; }
            [Required]
            public string rating { get; set; }
        }

    }
}
