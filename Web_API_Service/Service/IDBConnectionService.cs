using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web_API_Service.Models;

namespace Web_API_Service.Service {
	public interface IDBConnectionService {

		//Es stand for ElasticSearch
		Task<string> InsertInToEsMainDB(StringContent jsonString);
		Task<string> InsertInToEsErrorDB(StringContent jsonString);
		Task<string> GetFromEsMainDBWithQueryString(string commandString);
		Task<string> GetFromEsErrorDBWithQueryString(string commandString);

	}
}
