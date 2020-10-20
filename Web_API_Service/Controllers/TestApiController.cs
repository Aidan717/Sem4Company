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



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Service.Controllers {




	[Route("[controller]")]
	[ApiController]
	public class TestApiController : ControllerBase {
		
		public IMailService mailService;
		
		public TestApiController(IMailService mailService) {
			this.mailService = mailService;
        }
		


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

			//if (parameter != null) {
			using (var client = new HttpClient()) {
				var jsonstring = new StringContent(JsonSerializer.Serialize(parameter), Encoding.UTF8, "application/json");

				client.BaseAddress = new Uri("http://localhost:9200/" + chosenDB + "/_doc/");
				client.DefaultRequestHeaders.Accept.Clear();
				HttpResponseMessage response = await client.PostAsync("", jsonstring);

				if (response.IsSuccessStatusCode) {
					result = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync());
					return result;
				} else {
					// use mail service to report back
					return result;
				}
			}
		}

		[HttpPost("{chosenDB}/postwithfewcities")]
		public async Task<ActionResult<ResponseStatus>> PostParametersNotMet(string chosenDB, [FromBody] Schools._Source parameter) {

			var result = new ResponseStatus();
			string baseaddress = "";

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
						HttpResponseMessage response = await client.PostAsync("", jsonstring);

						if (response.IsSuccessStatusCode) {
							result = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync());
							return result;
						} else {
							HttpRequestException ex = new HttpRequestException("StatusCode: " + response.StatusCode);
							return result;
						}
                    } else {
						return result;
                    }
				}
			} catch(HttpRequestException ex) {
				MailController mailController = new MailController(mailService);
				var jsonstring = new String(JsonSerializer.Serialize(parameter));
				await mailController.SendWarningMail("PostParametersNotMet", jsonstring, baseaddress, ex.ToString());

				return result = new ResponseStatus("failed to connect");
			}
		}


		// send uri as string, id and exception to mailrequest generator.
		[HttpPut("{ChosenDB}/put/{id}")]

		public async Task<ActionResult<ResponseStatus>> Put(string ChosenDB, string id, [FromBody] SchoolsFake._Source parameter ) {
			var result = new ResponseStatus();
			string baseaddress = "";
			HttpResponseMessage response = new HttpResponseMessage();
			try {
				using (var client = new HttpClient()) {

				
				client.BaseAddress = new Uri("http://localhost:9200/" + ChosenDB + "/_doc/");
				baseaddress = client.BaseAddress.ToString();
				client.DefaultRequestHeaders.Accept.Clear();
				Debug.WriteLine("BaseAddress of client right hereeeeeeee: " + client.BaseAddress);
				var jsonstring = new StringContent(JsonSerializer.Serialize(parameter), Encoding.UTF8, "application/json");
				
				
					//Hvorfor bruger vi PostAsync og ikke PutAsync? cant remember
					response = await client.PostAsync("" + id, jsonstring);


					if (response.IsSuccessStatusCode) {
						result = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync());
						return result;
					} else {
						//Find the right exception to use
						HttpRequestException ex = new HttpRequestException("StatusCode: " + response.StatusCode);
						

						//Add exception to MailRequest and use Thundersnows SendMail Method
						MailController mailController = new MailController(mailService);
						ResponseStatus failedResponse = new ResponseStatus();

						var jsonstrings = new String(JsonSerializer.Serialize(parameter));
						await mailController.SendWarningMail("UpdateIndexWithId", jsonstrings, baseaddress, ex.ToString());

						failedResponse.result = "Failed to connnect: " + response.StatusCode.ToString();
						result = failedResponse;
						return result;

					}
				}
			} catch (HttpRequestException ex) {
				MailController mailController = new MailController(mailService);
				ResponseStatus failedResponse = new ResponseStatus();

				var jsonstrings = new String(JsonSerializer.Serialize(parameter));
				await mailController.SendWarningMail("UpdateIndexWithId", jsonstrings, baseaddress, ex.ToString());

				failedResponse.result = "Failed to connect: No Response";
				result = failedResponse;
				return result;
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
