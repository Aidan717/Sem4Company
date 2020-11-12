using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Web_API_Service.Data;

//some knew
namespace Web_API_Service {
	public class Program {

        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "clusterTestData.xlsx");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "errorClusteringModel.zip");

        public static void Main(string[] args) {
			CreateHostBuilder(args).Build().Run();

            ErrorClustering();

        }

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => {
					webBuilder.UseStartup<Startup>();
				});



        public static void ErrorClustering() {
            var mlContext = new MLContext();

            IDataView dataView = mlContext.Data.LoadFromTextFile<ErrorData>(_dataPath, hasHeader: false, separatorChar: ',');

            string featuresColumnName = "Features";
            var pipeline = mlContext.Transforms
                .Concatenate(featuresColumnName, "Errorname", "ErrorMessage")
                .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 2));

            var model = pipeline.Fit(dataView);

            using (var fileStream = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write)) {
                mlContext.Model.Save(model, dataView.Schema, fileStream);

                var predictor = mlContext.Model.CreatePredictionEngine<ErrorData, ClusterPrediction>(model);

                var prediction = predictor.Predict(TestErrorData.Error);
                Console.WriteLine($"Cluster: {prediction.PredictedError}");
                Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");
            }
        }
    }
}