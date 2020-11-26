using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System;
using System.IO;
using Microsoft.ML.Data;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using MachineLearning.Models;
using MachineLearning.Service;


namespace MachineLearning{
	public class MLForecaster {

			readonly static ISaveFile SaveFile = new SaveFile();
			static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "ElkTestModelResults.csv");
			static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "ModelData.zip");

		public void StartForcaster() {

			MLContext mlContext = new MLContext();

			IDataView dataView = mlContext.Data.LoadFromTextFile<ForecasterModel>(path: _dataPath, hasHeader: true, separatorChar: ';');
			int size = dataView.GetColumn<string>("month").Count();

			var forecastingPipeline = mlContext.Forecasting.ForecastBySsa(
				outputColumnName: "Forecast",
				inputColumnName: "errorTrainer",
				windowSize: 7,
				seriesLength: size,
				trainSize: size,
				horizon: 7,
				confidenceLevel: 0.95f,
				confidenceLowerBoundColumn: "LowerForecasting",
				confidenceUpperBoundColumn: "UpperForecasting"
				);

			SsaForecastingTransformer forecaster = forecastingPipeline.Fit(dataView);

			Evaluate(dataView, forecaster, mlContext);

			var forecastEngine = forecaster.CreateTimeSeriesEngine<ForecasterModel, ForecastingDataModel>(mlContext);

			forecastEngine.CheckPoint(mlContext, _modelPath);

			Forecast(forecastEngine);
		}

		static void Evaluate(IDataView dataView, ITransformer model, MLContext mlContext) {
			IDataView predictions = model.Transform(dataView);
			IEnumerable<float> actual = mlContext.Data.CreateEnumerable<ForecasterModel>(dataView, true).Select(observed => observed.errorTrainer);
			IEnumerable<float> forecast = mlContext.Data.CreateEnumerable<ForecastingDataModel>(predictions, true).Select(prediction => prediction.Forecast[0]);
			var metrics = actual.Zip(forecast, (actualValue, forecastValue) => actualValue - forecastValue);
			var MAE = metrics.Average(error => Math.Abs(error)); // Mean Absolute Error
			var RMSE = Math.Sqrt(metrics.Average(error => Math.Pow(error, 2))); // Root Mean Squared Error

			Console.WriteLine("Evaluation Metrics");
			Console.WriteLine("---------------------");
			Console.WriteLine($"Mean Absolute Error: {MAE:F2}");
			Console.WriteLine($"Root Mean Squared Error: {RMSE:F2}\n");

		}
			
		static void Forecast(TimeSeriesPredictionEngine<ForecasterModel, ForecastingDataModel> forecaster) {				
			ForecastingDataModel forecast = forecaster.Predict();				
			SaveFile.SaveForecastToCsvFile(forecast);
		}			
	}
}
