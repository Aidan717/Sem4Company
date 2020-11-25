using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachineLearning;

namespace Web_API_Service.Service
{
    public class MachineLearningService : IMachineLearning
    {
        MLAnomaly mLAnomaly = new MLAnomaly();
        MLForecast mLforecast = new MLForecast();
        public void CheckForSpikes()
        {
            mLAnomaly.MLCheckForSpikes();
            
        }
    }
}
