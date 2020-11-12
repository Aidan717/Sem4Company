using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace Web_API_Service.Data {
    public class ErrorData {
        [LoadColumn(3)]
        public int Error;
    }

    public class ClusterPrediction {
        [ColumnName("PredictedLabel")]
        public uint PredictedError;

        [ColumnName("Score")]
        public float[] Distances;
    }
}
