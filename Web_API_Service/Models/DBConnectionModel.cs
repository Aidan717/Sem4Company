using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
	public class dbConnectionModel {

		public string URI { get; set; }
		public string MainIndex { get; set; }
		public string ErrorIndex { get; set; }
		public string CustomIndex { get; set; }
		public string QueryString { get; set; }

	}
}
