using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.ML.Data;
using System.Diagnostics;

namespace MachineLearning {
    public class ClassificationModel {

        [LoadColumn(0)]
        public string timeStamp { get; set; }

        [LoadColumn(1)]
        public string name { get; set; }

        [LoadColumn(2)]
        public string errorForTrainer { get; set; }

        [LoadColumn(3)]
        public string exceptionstackTraceString { get; set; }

        [LoadColumn(4)]
        public string exceptioninnerExceptionMessage { get; set; }

        [LoadColumn(5)]
        public string exceptionfailedRecipient { get; set; }


        public ClassificationModel() { }

        public ClassificationModel(string str) {
            string[] stringColums = str.Split(';');
            int i = 0;
            foreach (PropertyInfo pi in this.GetType().GetProperties()) {
                pi.SetValue(this, System.Convert.ChangeType(stringColums[i], pi.PropertyType));
                i++;
            }
        }
    }

    public class ClassificationPredictionModel {
        [ColumnName("PredictedLabel")]
        public string errorForTrainer;
    }
}
