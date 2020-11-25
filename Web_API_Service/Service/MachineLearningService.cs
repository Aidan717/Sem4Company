using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachineLearning;

namespace Web_API_Service.Service
{
    public class MachineLearningService : IMachineLearning  {
        MLAnomaly ml = new MLAnomaly();
        
        
        public void CheckForSpikes()    {
            ml.MLCheckForSpikes();
            
        }
    }
}
