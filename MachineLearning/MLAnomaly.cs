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


namespace MachineLearning
{
    public class MLAnomaly
    {
        readonly static ISaveFile SaveFile = new SaveFile();
        //_dataPath has the path to the dataset used to train the model.
        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "ElkTestModel.csv");



        public string MLCheckForSpikes()
        {
            
            //The MLContext class is a starting point for all ML.NET operations, and initializing mlContext creates a new ML.NET environment that can be shared across the model creation workflow objects. It's similar, conceptually, to DBContext in Entity Framework.
            MLContext mlContext = new MLContext();

            //Data in ML.NET is represented as an IDataView class. IDataView is a flexible, efficient way of describing tabular data (numeric and text). Data can be loaded from a text file or from other sources (for example, SQL database or log files) to an IDataView object.
            //The LoadFromTextFile() defines the data schema and reads in the file. It takes in the data path variables and returns an IDataView.
            IDataView dataView = mlContext.Data.LoadFromTextFile<SpikesModel>(path: _dataPath, hasHeader: true, separatorChar: ',');
            int _docsize = dataView.GetColumn<string>("date").Count();

            DetectSpike(mlContext, _docsize, dataView);

            
            return null;
        }


        static void DetectSpike(MLContext mlContext, int docSize, IDataView productSales)
        {
            var iidSpikeEstimator = mlContext.Transforms.DetectIidSpike(outputColumnName: nameof(SpikesPrediction.Prediction), inputColumnName: nameof(SpikesModel.totalErrors), confidence: 95, pvalueHistoryLength: docSize / 4);
            ITransformer iidSpikeTransform = iidSpikeEstimator.Fit(CreateEmptyDataView(mlContext));
            IDataView transformedData = iidSpikeTransform.Transform(productSales);
            var predictions = mlContext.Data.CreateEnumerable<SpikesPrediction>(transformedData, reuseRowObject: false);
            Console.WriteLine("Alert\tScore\tP-Value");

            int i = 0;
            List<string> r = productSales.GetColumn<string>("date").ToList();

            foreach (var p in predictions)
            {

                var results = $"{p.Prediction[0]}\t{p.Prediction[1]:f2}\t{p.Prediction[2]:F2}\t{r[i]}";

                if (p.Prediction[0] == 1)
                {
                    results += " <-- Spike detected";
                }
                Console.WriteLine(results);
                i++;
            }
            Console.WriteLine("");

            SaveFile.SaveFileToCsv(predictions, r);
        }



        //The CreateEmptyDataView() produces an empty data view object with the correct schema to be used as input to the IEstimator.Fit() method.
        static IDataView CreateEmptyDataView(MLContext mlContext)
        {
            //Create empty DataView. We just need the schema to call Fit() for the time series transforms
            IEnumerable<SpikesModel> enumerableData = new List<SpikesModel>();
            return mlContext.Data.LoadFromEnumerable(enumerableData);
        }
    }
}
