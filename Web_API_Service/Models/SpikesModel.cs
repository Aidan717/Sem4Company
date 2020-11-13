using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models
{
    //SpikesModel specifies an input data class. The LoadColumn attribute specifies which columns (by column index) in the dataset should be loaded.
    public class SpikesModel
    {
        [LoadColumn(0)]
        public string date;

        [LoadColumn(1)]
        public float totalErrors;
    }
}

//SpikesPrediction specifies the prediction data class. For anomaly detection, the prediction consists of an alert to indicate whether there is an anomaly, a raw score, and p-value. The closer the p-value is to 0, the more likely an anomaly has occurred.
public class SpikesPrediction
{

    //vector to hold alert,score,p-value values
    [VectorType(3)]
    public double[] Prediction { get; set; }
}

