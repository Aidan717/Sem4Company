using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
	public class dbConnectionModel {

		public string uRI { get; set; }
		public string mainIndex { get; set; }
		public string errorIndex { get; set; }
		public string customIndex { get; set; }
		public string queryString { get; set; }

	}
}
