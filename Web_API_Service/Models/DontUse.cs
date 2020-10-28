using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
	public class DontUse {


		public class Rootobject {
			public int took { get; set; }
			public bool timed_out { get; set; }
			public _Shards _shards { get; set; }
			public Hits hits { get; set; }
		}

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
			public string username { get; set; }
			public int userId { get; set; }
			public string exceptionMessage { get; set; }
		}



	}
}
