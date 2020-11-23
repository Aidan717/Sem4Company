using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
	public class ElasticConnection {

		public string ElasticURI { get; set; }
		public string ElasticMainIndex { get; set; }
		public string ElasticErrorIndex { get; set; }
		public string ElasticCustomIndex { get; set; }
		public string ElasticCommandString { get; set; }

	}
}
