using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Web_API_Service.Models;
using System.Text.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Service.Controllers {
	[Route("[controller]")]
	[ApiController]
	public class TestApiController: ControllerBase {
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
                string tempresult = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<OpenWeatherMapsApi>(await response.Content.ReadAsStringAsync());
                if (response.IsSuccessStatusCode) {
                    return result;
                } else {
                    return result;
                }
            }
		}

		//GET <OpenWeatherMapsApiController>/forecast/APIQuery
		[HttpGet("addr/{AddressAPIQuery}")]
		public async Task<ActionResult<AddressModel>> GetAddress(string AddressAPIQuery) {

			using (var client = new HttpClient()) {
				var result = new AddressModel();
				client.BaseAddress = new Uri("https://dawa.aws.dk/adresser");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync("?q=" + AddressAPIQuery);
				string tempresult = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<AddressModel>(await response.Content.ReadAsStringAsync());
				if (response.IsSuccessStatusCode) {
					return result;
				} else {
					return result;
				}
			}
		}

		[HttpGet("elkLog")]
		public async Task<ActionResult<ELKLog>> GetLog(string elkAPIQuery) {

			using (var client = new HttpClient()) {
				var result = new ELKLog();
				client.BaseAddress = new Uri("http://localhost:9600");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync("");
				string tempresult = await response.Content.ReadAsStringAsync();
				result = JsonSerializer.Deserialize<ELKLog>(await response.Content.ReadAsStringAsync());
				if (response.IsSuccessStatusCode) {
					return result;
				} else {
					return result;
				}
			}
		}

		[HttpGet("elks")]
		public async Task<ActionResult<ELKSearch>> GetElk(string elkAPIQuery) {

			using (var client = new HttpClient()) {
				var result = new ELKSearch();
				client.BaseAddress = new Uri("http://localhost:9200");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync("");
				string tempresult = await response.Content.ReadAsStringAsync();
				result = JsonSerializer.Deserialize<ELKSearch>(await response.Content.ReadAsStringAsync());
				if (response.IsSuccessStatusCode) {
					return result;
				} else {
					return result;
				}
			}
		}


		// POST api/<OpenWeatherMapsApiController>
		[HttpPost]
		public void Post([FromBody] string value) {
		}

		// PUT api/<OpenWeatherMapsApiController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value) {
		}

		// DELETE api/<OpenWeatherMapsApiController>/5
		[HttpDelete("{id}")]
		public void Delete(int id) {
		}
	}
}
