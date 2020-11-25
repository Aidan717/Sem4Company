using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MachineLearning.Models;

namespace MachineLearning.Service
{
    public class SaveFile : ISaveFile
    {
        static readonly string _anomalyModelPath = Path.Combine(Environment.CurrentDirectory, "Data", "ElkTestModelResults.csv");
        static readonly string _forecastingModelPath = Path.Combine(Environment.CurrentDirectory, "Data", "forecastingModelResults.csv");
        public void SaveFileToCsv(IEnumerable<SpikesPrediction> prediction, List<string> r)
        {
            using (StreamWriter writer = File.CreateText(_anomalyModelPath))
            {
                int i = 0;
                writer.Write("Date;Alert;numError;P - value;\r");
                foreach (var p in prediction)
                {
                    writer.Write($"{r[i]};{p.Prediction[0]};{Convert.ToInt32(p.Prediction[1])};{p.Prediction[2]:F2}\r");
                    Debug.WriteLine("Printing line: " + i);
                    i++;
                }
                Debug.WriteLine("Done!");
            }
        }
        public void SaveForecastToCsvFile(ForecastingDataModel forecast) {
            using (StreamWriter writer = File.CreateText(_forecastingModelPath)) {
                
                writer.Write("Forecast;Lowerforecast;UpperForecasting;\r");
                for (int i = 0; i < forecast.Forecast.Length; i++) {
                    writer.Write($"{forecast.Forecast[i]}; {forecast.LowerForecasting[i]}; {forecast.UpperForecasting[i]}\r");
                    Debug.WriteLine($"{forecast.Forecast[i]}; {forecast.LowerForecasting[i]}; {forecast.UpperForecasting[i]}");
                    i++;
                }
                Debug.WriteLine("Done!");
            }
        }

    }
}
