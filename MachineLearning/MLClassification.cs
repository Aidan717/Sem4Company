using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using MachineLearning.Models;
using System.Diagnostics;

namespace MachineLearning {
    public class MLClassification{

        private static string _appPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        //private static string _trainDataPath => Path.Combine(_appPath, "..", "..", "..", "Data", "GroupingTraining.csv");
        static readonly string _trainDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "GroupHalfTraining.csv");

        //private static string _testDataPath => Path.Combine(_appPath, "..", "..", "..", "Data", "GroupingTest.csv");
        static readonly string _testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "GroupHalfTest.csv");

        //private static string _modelPath => Path.Combine(_appPath, "..", "..", "..", "Models", "model.zip");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "classificationDataModel.zip");

        private static MLContext _mlContext;
        private static PredictionEngine<ClassificationModel, ClassificationPredictionModel> _predEngine;
        private static ITransformer _trainedModel;
        static IDataView _trainingDataView;


        public void ClassificationStart() {
            _mlContext = new MLContext(seed: 1);

            _trainingDataView = _mlContext.Data.LoadFromTextFile<ClassificationModel> (_trainDataPath, hasHeader: true, separatorChar: ';');

            var pipeline = ProcessData();

            var trainingPipeline = BuildAndTrainModel(_trainingDataView, pipeline);

            Evaluate(_trainingDataView.Schema);

            //PredictIssue();
        }

        public static IEstimator<ITransformer> ProcessData() {
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "errorForTrainer", outputColumnName: "Label")
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "exceptionstackTraceString", outputColumnName: "exceptionstackTraceStringFeaturized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "exceptioninnerExceptionMessage", outputColumnName: "exceptioninnerExceptionMessageFeaturized"))
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "exceptionfailedRecipient", outputColumnName: "exceptionfailedRecipientFeaturized"))
                .Append(_mlContext.Transforms.Concatenate("Features", "exceptionstackTraceStringFeaturized", "exceptioninnerExceptionMessageFeaturized", "exceptionfailedRecipientFeaturized"))
                .AppendCacheCheckpoint(_mlContext);

            return pipeline;
        }

        public static IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline) {
            var trainingPipeline = pipeline.Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            _trainedModel = trainingPipeline.Fit(trainingDataView);

            _predEngine = _mlContext.Model.CreatePredictionEngine<ClassificationModel, ClassificationPredictionModel>(_trainedModel);

            //ClassificationModel issue = new ClassificationModel() {
            //    exceptionstackTraceString = "",
            //    exceptioninnerExceptionMessage = ""
            //    //error = 1

            //    //timeStamp = "Nov 11 2012 @ 13:59:50.000",
            //    //name = "Bjarne"
            //    //error = 0
            //};

            //var prediction = _predEngine.Predict(issue);

            //Debug.WriteLine($"=============== Single Prediction just-trained-model - Result: {prediction.errorForTrainer} ===============");

            return trainingPipeline;
        }

        public static void Evaluate(DataViewSchema trainingDataViewSchema) {
            var testDataView = _mlContext.Data.LoadFromTextFile<ClassificationModel>(_testDataPath, hasHeader: true, separatorChar: ';');

            var testMetrics = _mlContext.MulticlassClassification.Evaluate(_trainedModel.Transform(testDataView));

            Debug.WriteLine($"*************************************************************************************************************");
            Debug.WriteLine($"*       Metrics for Multi-class Classification model - Test Data     ");
            Debug.WriteLine($"*------------------------------------------------------------------------------------------------------------");
            Debug.WriteLine($"*       MicroAccuracy:    {testMetrics.MicroAccuracy:F3}");
            Debug.WriteLine($"*       MacroAccuracy:    {testMetrics.MacroAccuracy:F3}");
            Debug.WriteLine($"*       LogLoss:          {testMetrics.LogLoss:#.###}");
            Debug.WriteLine($"*       LogLossReduction: {testMetrics.LogLossReduction:#.###}");
            Debug.WriteLine($"*************************************************************************************************************");

            SaveModelAsFile(_mlContext, trainingDataViewSchema, _trainedModel);
        }

        private static void SaveModelAsFile(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model) {
            mlContext.Model.Save(model, trainingDataViewSchema, _modelPath);


        }

        private static void PredictIssue() {
            ITransformer loadedModel = _mlContext.Model.Load(_modelPath, out var modelInputSchema);

            ClassificationModel singleIssue = new ClassificationModel() { exceptionstackTraceString = "not allowed to send", exceptioninnerExceptionMessage = "port CancellationToken" };

            _predEngine = _mlContext.Model.CreatePredictionEngine<ClassificationModel, ClassificationPredictionModel>(loadedModel);

            var prediction = _predEngine.Predict(singleIssue);

            Console.WriteLine($"=============== Single Prediction - Result: {prediction.errorForTrainer} ===============");
        }

        public Boolean ClassificationPrediction(string PredictionLine) {
            //ClassificationStart();

            Boolean pass = false;

            _mlContext = new MLContext(seed: 0);

            ITransformer loadedModel = _mlContext.Model.Load(_modelPath, out var modelInputSchema);

            ClassificationModel clm = new ClassificationModel(PredictionLine);

            _predEngine = _mlContext.Model.CreatePredictionEngine<ClassificationModel, ClassificationPredictionModel>(loadedModel);

            var prediction = _predEngine.Predict(clm);

            Debug.WriteLine($"=============== Single Prediction - Result: {prediction.errorForTrainer} ===============");

            if (prediction.errorForTrainer.Equals("1")) {
                pass = true;
            }

            return pass;
        }
    }
}
