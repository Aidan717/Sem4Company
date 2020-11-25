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

		private readonly DBConnectionModel _DBconSetting;
		private HttpResponseMessage response = new HttpResponseMessage();
		private ResponseStatus respond = new ResponseStatus();
		private string respondString;

		public DBConnectionService(IOptions<DBConnectionModel> DBconection) {
			_DBconSetting = DBconection.Value;
		}

		public async Task<string> InsertInToMainDB(StringContent jsonString) {
			
			try {
				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/maindb/_doc/");
					client.BaseAddress = new Uri($"{_DBconSetting.uRI}/{_DBconSetting.mainIndex}/{_DBconSetting.queryString}");					
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
		public async Task<string> InsertInToErrorDB(StringContent jsonString) {
			
			try {

				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/errodb/_doc/");
					client.BaseAddress = new Uri($"{_DBconSetting.uRI}/{_DBconSetting.errorIndex}/{_DBconSetting.queryString}");
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



		public async Task<string> GetFromMainDBWithQueryString(string queryString) {
			try {
				_DBconSetting.queryString = queryString;
				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/maindb/_doc/");
					client.BaseAddress = new Uri($"{_DBconSetting.uRI}/{_DBconSetting.mainIndex}/{_DBconSetting.queryString}");
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.GetAsync("");

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

		public async Task<string> GetFromErrorDBWithQueryString(string queryString) {
			try {
				_DBconSetting.queryString = queryString;
				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/errodb/_doc/");
					client.BaseAddress = new Uri($"{_DBconSetting.uRI}/{_DBconSetting.errorIndex}/{_DBconSetting.queryString}");
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.GetAsync("");

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

		public async Task<string> GetHealthFromMainDB() {
			try {
				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/_cluster/health/maindb");
					client.BaseAddress = new Uri($"{_DBconSetting.uRI}/_cluster/health/{_DBconSetting.mainIndex}");
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.GetAsync("");

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
		public async Task<string> GetHealthFromErrorDB() {
			try {
				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/_cluster/health/errordb");
					client.BaseAddress = new Uri($"{_DBconSetting.uRI}/_cluster/health/{_DBconSetting.errorIndex}");
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.GetAsync("");

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
	}
}
