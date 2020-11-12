using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace Web_API_Service.Data {
    public class ErrorData {
        [LoadColumn(0)]
        public string ErrorName;

        [LoadColumn(1)]
        public string ErrorMessage;
    }

    public class ClusterPrediction {
        [ColumnName("PredictedLabel")]
        public uint PredictedError;

        [ColumnName("Score")]
        public float[] Distances;
    }
}
