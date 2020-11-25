using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MachineLearning.Models;
using MachineLearning.Service;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace MachineLearning.Models
{
    class MLForecast
    {

        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "ElkTestModel.csv");

        public void Hello()
        {

            Debug.WriteLine("Hello World");
        }
    }
}
