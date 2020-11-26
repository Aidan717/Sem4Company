using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Service
{
    interface IMachineLearning
    {
        public void CheckForSpikes();
        public void Forecaster();
        public Boolean Classification(string line);
    }
}
