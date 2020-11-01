using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Web_API_Service.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Web_API_Service.Controllers;
using Web_API_Service.Service;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Converters;
using Web_API_Service.Utility;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Service.Controllers {




	[Route("[controller]")]
	[ApiController]
	public class TestApiController : ControllerBase {




		// GET: api/<OpenWeatherMapsApiController>
		[HttpGet]
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET <OpenWeatherMapsApiController>/weather/APIQuery
		[HttpGet("weather/{APIQuery}")]
		public async Task<ActionResult<OpenWeatherMapsApi>> GetWeather(string APIQuery) {

			using (var client = new HttpClient()) {
				var result = new OpenWeatherMapsApi();
				string key = "e7f9dce3dd5e96bc3faf4f5ca8014fcb";
				client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync("?q=" + APIQuery + "&appid=" + key + "");
				if (response.IsSuccessStatusCode) {
					result = JsonSerializer.Deserialize<OpenWeatherMapsApi>(await response.Content.ReadAsStringAsync());
					return result;
				} else {
					return result;
				}
			}
		}

		[HttpGet("wp/{APIQuery}")]
		public async Task<ActionResult<forcast>> GetWeathers(string APIQuery) {

			using (var client = new HttpClient()) {
				var result = new forcast();
				string key = "e7f9dce3dd5e96bc3faf4f5ca8014fcb";
				client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync("forecast?q=aalborg&appid=e7f9dce3dd5e96bc3faf4f5ca8014fcb");

				if (response.IsSuccessStatusCode) {
					result = JsonSerializer.Deserialize<forcast>(await response.Content.ReadAsStringAsync());
					return result;
				} else {
					return result;
				}
			}
		}

		[HttpGet("db/{chosenDB}/{SearchParameter}")]
		public async Task<ActionResult<Schools>> GetSchool(string chosenDB, string SearchParameter) {

			using (var client = new HttpClient()) {
				var result = new Schools();
				client.BaseAddress = new Uri("http://localhost:9200/" + chosenDB + "/_search");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync("?q=" + SearchParameter);

				if (response.IsSuccessStatusCode) {
					result = JsonSerializer.Deserialize<Schools>(await response.Content.ReadAsStringAsync());
					return result;
				} else {
					return result;
				}
			}
		}

		[HttpGet("schools/del/{id}")]
		public async Task<ActionResult<ResponseStatus>> DelSchool(string id) {

			using (var client = new HttpClient()) {
				var result = new ResponseStatus();
				client.BaseAddress = new Uri("http://localhost:9200/schools/_doc/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.DeleteAsync(id);

				if (response.IsSuccessStatusCode) {
					result = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync());
					return result;
				} else {
					return result;
				}
			}
		}


		// POST api/<OpenWeatherMapsApiController>
		[HttpPost("{chosenDB}/post")]
		public async Task<ActionResult<ResponseStatus>> Post(string chosenDB, [FromBody] Schools._Source parameter) {
			var result = new ResponseStatus();
			string baseaddress = "";
			HttpResponseMessage response = new HttpResponseMessage();

			try {
				//if (parameter != null) {
				using (var client = new HttpClient()) {
					var jsonstring = new StringContent(JsonSerializer.Serialize(parameter), Encoding.UTF8, "application/json");

					client.BaseAddress = new Uri("http://localhost:9200/" + chosenDB + "/_doc/");
					baseaddress = client.BaseAddress.ToString();
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.PostAsync("", jsonstring);

					if (response.IsSuccessStatusCode) {
						result = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync());
						return result;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}
			} catch (HttpRequestException ex) {
				MailService warningMail = new MailService();
				var jsonstrings = new String(JsonSerializer.Serialize(parameter));
                await warningMail.SendWarningEmailAsync("Post", jsonstrings, baseaddress, ex.Message);

                return result = new ResponseStatus("failed to connect" + ex.Message);
			}
		}

		[HttpPost("{chosenDB}/postwithfewcities")]
		public async Task<ActionResult<ResponseStatus>> PostParametersNotMet(string chosenDB, [FromBody] Schools._Source parameter) {

			var result = new ResponseStatus();
			string baseaddress = "";
			HttpResponseMessage response = new HttpResponseMessage();

			try {
				using (var client = new HttpClient()) {

					String[] avalibleCities = { "Aalborg", "Brønderslev", "Hjørring", "Skagen" };
					Boolean avalibleCityCheck = false;

					foreach (String element in avalibleCities) {
						if (parameter.city == element) {
							avalibleCityCheck = true;
						}
					}

					client.BaseAddress = new Uri("http://localhost:9200/" + chosenDB + "/_doc/");
					baseaddress = client.BaseAddress.ToString();
					client.DefaultRequestHeaders.Accept.Clear();

					if (avalibleCityCheck == true) {
						var jsonstring = new StringContent(JsonSerializer.Serialize(parameter), Encoding.UTF8, "application/json");
						response = await client.PostAsync("", jsonstring);

						if (response.IsSuccessStatusCode) {
							result = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync());
							return result;
						} else {
							throw new HttpRequestException("statusCode: " + response.StatusCode);
						}

					} else {
						throw new HttpRequestException("the chosen city is not on the list");
					}
				}
			} catch(HttpRequestException ex) {
				MailService warningMail = new MailService();

				var jsonstrings = new String(JsonSerializer.Serialize(parameter));
				await warningMail.SendWarningEmailAsync("PostParametersNotMet", jsonstrings, baseaddress, ex.Message);

				return result = new ResponseStatus(ex.Message);
			}
		}


		// send uri as string, id and exception to mailrequest generator.
		[HttpPut("{ChosenDB}/put/{id}")]

		public async Task<ActionResult<ResponseStatus>> Put(string ChosenDB, string id, [FromBody] DBSchema parameter ) {
			var result = new ResponseStatus();
			string baseaddress = "";
			HttpResponseMessage response = new HttpResponseMessage();
			try {
				using (var client = new HttpClient()) {

				
					client.BaseAddress = new Uri("http://localhost:9200/" + ChosenDB + "/_doc/");
					baseaddress = client.BaseAddress.ToString();
					client.DefaultRequestHeaders.Accept.Clear();
					
					var jsonstring = new StringContent(JsonSerializer.Serialize(parameter), Encoding.UTF8, "application/json");
					response = await client.PutAsync("" + id, jsonstring);

					if (response.IsSuccessStatusCode) {
						result = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync());
						return result;
					} else {
						throw new HttpRequestException("StatusCode: " + response.StatusCode);
					}
				}
			} catch (HttpRequestException ex) {
				DBSchemaCopy ds = new DBSchemaCopy();
				
				
				MailService warningMail = new MailService();
				
				var jsonstrings = new String(JsonSerializer.Serialize(parameter));
				await warningMail.SendWarningEmailAsync("UpdateIndexWithId", jsonstrings, baseaddress, ex.Message);

				return result = new ResponseStatus(ex.Message);
			}
		}

		[HttpGet("elkl")]
		public async Task<ActionResult<ElkLog>> Getelkl(string APIQuery) {

			using (var client = new HttpClient()) {
				var result = new ElkLog();
				client.BaseAddress = new Uri("http://localhost:9600");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync("");

				

				if (response.IsSuccessStatusCode) {

					result = JsonSerializer.Deserialize<ElkLog>(await response.Content.ReadAsStringAsync());
					return result;
				} else {
					return result;
				}
			}
		}


		[HttpGet("dbschema/{chosenDB}/{SearchParameter}")]
		public async Task<ActionResult<DBSchemaCopy>> GetDbSchema(string chosenDB, string SearchParameter) {

			using (var client = new HttpClient()) {
				var result = new DBSchemaCopy();
				//var datetimeconverter = new DateTimeConverter();
				client.BaseAddress = new Uri("http://localhost:9200/" + chosenDB + "/_search");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync("?q=" + SearchParameter);

				if (response.IsSuccessStatusCode) {

					var options = new JsonSerializerOptions {
						Converters = { new DateTimeConverter() }
					};

					result = JsonSerializer.Deserialize<DBSchemaCopy>(await response.Content.ReadAsStringAsync(), options);

					DateTime limit = 

					foreach (DBSchemaCopy._Source s in result.hits.hits) {
						s.timestamp.
					}
					//send to method for checking exceptions with datetime
					//if 3 or more within 5 minutes, send email with exception name
					//else, return result.

					return result;
				} else {
					return result;
				}
			}
		}



		//DOES NOT WORK
		//GET<OpenWeatherMapsApiController>
		//[HttpGet("addr/{AddressAPIQuery}")]
		//public async Task<ActionResult<Address>> GetAddress(string AddressAPIQuery) {


		//	using (var client = new HttpClient()) {
		//		var result = new Address();
		//		client.BaseAddress = new Uri("https://dawa.aws.dk/adresser");
		//		client.DefaultRequestHeaders.Accept.Clear();
		//		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		//		HttpResponseMessage response = await client.GetAsync("?q=" + AddressAPIQuery);
		//		string test = await response.Content.ReadAsStringAsync();


		//		var options = new JsonSerializerOptions {
		//			PropertyNameCaseInsensitive = true,
		//			Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
		//		};
		//		options.Converters.Add(new LongtoStringConverter());
		//		var netest = JsonSerializer.Deserialize<Address>(test, options);

		//		var newtest = JsonSerializer.Deserialize<IEnumerable<string>>(test, options);
		//		Address result = JsonSerializer.Deserialize<Address>(test);


		//		var result = Enumerable.Range(1, 5).Select(newtest => new Address);

		//		return newtest;
		//		return result;


		//		if (response.IsSuccessStatusCode) {
		//			return result;
		//		} else {
		//			return netest;
		//		}
		//	}
		//}

		// POST api/<OpenWeatherMapsApiController>
		//[HttpPost]
		//public void Post([FromBody] string value) {
		//}

		// PUT api/<OpenWeatherMapsApiController>/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value) {
		//}

		// DELETE api/<OpenWeatherMapsApiController>/5
		//[HttpDelete("{id}")]
		//public void Delete(int id) {
		//}





	}
}
