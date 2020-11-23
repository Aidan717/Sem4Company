using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web_API_Service.Models;

namespace Web_API_Service.Service {
	public interface IElasticConnectionService {

		//Es stand for ElasticSearch
		Task<string> InsertIndToEsMainDB(StringContent jsonString);
		Task<string> InsertIndToEsErrorDB(StringContent jsonString);
		Task<string> GetFromEsMainDBWithCommandstring(string commandString);
		Task<string> GetFromEsErrorDBWithCommandstring(string commandString);

	}
}
