using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MachineLearning.Service
{
    public class SaveFile : ISaveFile
    {
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "ElkTestModelResults.csv");
        public void SaveFileToCsv(IEnumerable<SpikesPrediction> prediction, List<string> r)
        {
            using (StreamWriter writer = File.CreateText(_modelPath))
            {
                int i = 0;
                writer.Write("Date,Alert,numError,P - value,\r");
                foreach (var p in prediction)
                {
                    writer.Write($"{r[i]},{p.Prediction[0]},{Convert.ToInt32(p.Prediction[1])},{p.Prediction[2]:F2}\r");
                    Debug.WriteLine("Printing line: " + i);
                    i++;
                }
                Debug.WriteLine("Done!");
            }
        }

    }
}
