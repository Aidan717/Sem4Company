using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachineLearning;

namespace Web_API_Service.Service
{
    public class MachineLearningService : IMachineLearning {
        MLAnomaly mLAnomaly = new MLAnomaly();
        MLForecaster mLforecaster = new MLForecaster();
        MLClassification mLClassification = new MLClassification();
        
        public void CheckForSpikes() {
            mLAnomaly.MLCheckForSpikes();            
        }

        public void Forecaster() {
            mLforecaster.StartForcaster();
		}

        public Boolean Classification(string line) {
            return mLClassification.ClassificationPrediction(line);
        }
    }
}
