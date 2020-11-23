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
	public class ElasticConnectionService : IElasticConnectionService {		

		private readonly ElasticConnection _DBconSetting;
		private HttpResponseMessage response = new HttpResponseMessage();
		private ResponseStatus respond = new ResponseStatus();
		private string respondString;

		public ElasticConnectionService(IOptions<ElasticConnection> DBconection) {
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
					client.BaseAddress = new Uri($"{_DBconSetting.ElasticURI}/{_DBconSetting.ElasticMainIndex}/{_DBconSetting.ElasticCommandString}");					
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
					client.BaseAddress = new Uri($"{_DBconSetting.ElasticURI}/{_DBconSetting.ElasticErrorIndex}/{_DBconSetting.ElasticCommandString}");
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.PostAsync("", jsonString);

					if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							Converters = { new DateTimeConverter() }
						};
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



		public async Task<string> GetFromEsMainDBWithCommandstring(string commandString) {
			try {
				_DBconSetting.ElasticCommandString = commandString;
				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/errodb/_doc/");
					client.BaseAddress = new Uri($"{_DBconSetting.ElasticURI}/{_DBconSetting.ElasticMainIndex}/{_DBconSetting.ElasticCommandString}");
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.GetAsync("");

					Debug.WriteLine("Uri:" + client.BaseAddress.ToString());
					if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							Converters = { new DateTimeConverter() }
						};
						respondString = JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync(), option);

						return respondString;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}

			} catch (HttpRequestException ex) {
				throw ex;
			}
		}

		public async Task<string> GetFromEsErrorDBWithCommandstring(string commandString) {
			try {
				_DBconSetting.ElasticCommandString = commandString;
				using (var client = new HttpClient()) {

					//next line should look like something like this is the default havent been changed
					//client.BaseAddress = new Uri("http://localhost:9200/errodb/_doc/");
					client.BaseAddress = new Uri($"{_DBconSetting.ElasticURI}/{_DBconSetting.ElasticErrorIndex}/{_DBconSetting.ElasticCommandString}");
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.GetAsync("");

					if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							Converters = { new DateTimeConverter() }
						};
						respondString = JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync(), option);

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
