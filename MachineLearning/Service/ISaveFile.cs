using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachineLearning.Models;
namespace MachineLearning.Service
{
    interface ISaveFile
    {
        public void SaveFileToCsv(IEnumerable<SpikesPrediction> predictions, List<string> r);
        public void SaveForecastToCsvFile(ForecastingDataModel forecast);
    }
}
