using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineLearning.Service
{
    interface ISaveFile
    {
        public void SaveFileToCsv(IEnumerable<SpikesPrediction> predictions, List<string> r);
    }
}
