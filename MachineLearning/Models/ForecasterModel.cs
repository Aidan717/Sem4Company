using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;
using System.Collections;

namespace MachineLearning.Models {
	class ForecasterModel {

		[LoadColumn(0)]
		public string month { get; set; }
		//[LoadColumn(4)]
		//public float year { get; set; }
		[LoadColumn(2)]
		public float errorTrainer { get; set; }

	}
}
