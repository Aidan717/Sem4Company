using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web_API_Service.Models;

namespace Web_API_Service.Service {
	public interface IDBConnectionService {

		Task<string> InsertInToMainDB(StringContent jsonString);
		Task<string> InsertInToErrorDB(StringContent jsonString);
		Task<string> GetFromMainDBWithQueryString(string commandString);
		Task<string> GetFromErrorDBWithQueryString(string commandString);
		Task<string> GetHealthFromMainDB();
		Task<string> GetHealthFromErrorDB();

	}
}
