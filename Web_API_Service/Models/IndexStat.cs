using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_API_Service.Models {
    public class IndexStat {

        public string id { get; set; }
        public string status { get; set; }
        public string index { get; set; }
     
    }
}
