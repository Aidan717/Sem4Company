using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning.Models {
	public class ForecastingDataModel {
		public float[] Forecast { get; set; }
		public float[] UpperForecasting { get; set; }
		public float[] LowerForecasting { get; set; }

	}
}
