using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.ML.Data;

namespace Web_API_Service.Models {
    public class Classification {

        public string timeStamp { get; set; }

        public string name { get; set; }

        public string errorForTrainer { get; set; }

        public string exceptionstackTraceString { get; set; }

        public string exceptioninnerExceptionessage { get; set; }

        //public string activitiesExceptionsMessage { get; set; }

        //public string exceptionDetailMessage { get; set; }
    }
}
