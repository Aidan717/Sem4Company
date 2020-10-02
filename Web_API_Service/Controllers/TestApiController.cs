﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Web_API_Service.Models;
using Web_API_Service.util;
using System.Text.Json;
using System.Text.Json.Serialization;


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

		[HttpGet("elks")]
		public async Task<ActionResult<Elksearch>> Getelks(string APIQuery) {

			using (var client = new HttpClient()) {
				var result = new Elksearch();
				client.BaseAddress = new Uri("http://localhost:9200");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync("");
				
				if (response.IsSuccessStatusCode) {
					result = JsonSerializer.Deserialize<Elksearch>(await response.Content.ReadAsStringAsync());
					return result;
				} else {
					return result;
				}
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