using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.ML.Data;

namespace Web_API_Service.Models {
    public class Classification {
        
        public string timestamp { get; set; }

        public string userName { get; set; }

        public string errorForTrainer { get; set; }

        [JsonPropertyName("exception.stackTraceString")]
        public string exceptionstackTraceString { get; set; }

        [JsonPropertyName("exception.innerException.message")]
        public string exceptioninnerExceptionMessage { get; set; }

        [JsonPropertyName("exception.failedRecipient")]
        public string exceptionfailedRecipient { get; set; }

        //public string activitiesExceptionsMessage { get; set; }

        //public string exceptionDetailMessage { get; set; }
    }
}
