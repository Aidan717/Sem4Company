using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API_Service.Models;

namespace Web_API_Service.Service {
	interface IDBInfoGenerater {
		DBSchema._Source GetNewData();
	}
}
