using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Web_API_Service.Models;
using Web_API_Service.Utility;

namespace Web_API_Service.Service {
	public class DBConnectionService : IDBConnectionService {		

		private readonly dbConnectionModel _DBconSetting;
		private HttpResponseMessage response = new HttpResponseMessage();
		private ResponseStatus respond = new ResponseStatus();
		private string respondString;

		public DBConnectionService(IOptions<dbConnectionModel> DBconection) {
			_DBconSetting = DBconection.Value;
		}

		//Es stand for ElasticSearch
		public async Task<string> InsertInToEsMainDB(StringContent jsonString) {
			//HttpResponseMessage response = new HttpResponseMessage();
			//ResponseStatus respond = new ResponseStatus();
			
			try {
				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/maindb/_doc/");
					client.BaseAddress = new Uri($"{_DBconSetting.URI}/{_DBconSetting.MainIndex}/{_DBconSetting.QueryString}");					
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.PostAsync("", jsonString);					

					if (response.IsSuccessStatusCode) {

						respondString = await response.Content.ReadAsStringAsync();

						return respondString;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}


			} catch (HttpRequestException ex) {
				throw ex;
			}
		}

		//this method only handle insert to errorDB
		public async Task<string> InsertInToEsErrorDB(StringContent jsonString) {
			
			try {

				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/errodb/_doc/");
					client.BaseAddress = new Uri($"{_DBconSetting.URI}/{_DBconSetting.MainIndex}/{_DBconSetting.QueryString}");
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.PostAsync("", jsonString);

					if (response.IsSuccessStatusCode) {

						respondString = await response.Content.ReadAsStringAsync();

						return respondString;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}

			} catch (HttpRequestException ex) {
				throw ex;
			}
		} 



		public async Task<string> GetFromEsMainDBWithQueryString(string commandString) {
			try {
				_DBconSetting.QueryString = commandString;
				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/maindb/_doc/");
					client.BaseAddress = new Uri($"{_DBconSetting.URI}/{_DBconSetting.MainIndex}/{_DBconSetting.QueryString}");
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.GetAsync("");

					Debug.WriteLine("Uri:" + client.BaseAddress.ToString());
					if (response.IsSuccessStatusCode) {

						respondString = await response.Content.ReadAsStringAsync();

						return respondString;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}

			} catch (HttpRequestException ex) {
				throw ex;
			}
		}

		public async Task<string> GetFromEsErrorDBWithQueryString(string commandString) {
			try {
				_DBconSetting.QueryString = commandString;
				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/errodb/_doc/");
					client.BaseAddress = new Uri($"{_DBconSetting.URI}/{_DBconSetting.MainIndex}/{_DBconSetting.QueryString}");
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.GetAsync("");

					if (response.IsSuccessStatusCode) {

						respondString = JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());

						return respondString;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}

			} catch (HttpRequestException ex) {
				throw ex;
			}
		}
	}
}
