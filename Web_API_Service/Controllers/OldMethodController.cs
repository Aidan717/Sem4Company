using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Web_API_Service.Models;
using Web_API_Service.Service;
using Web_API_Service.Utility;

namespace Web_API_Service.Controllers {
	public class OldMethodController : Controller {


		public readonly IMailService _mailService;
		public readonly IDBConnectionService _DBConnection;

		public ResponseStatus respondStatus = new ResponseStatus();
		public HttpResponseMessage response = new HttpResponseMessage();

		public OldMethodController(IMailService mailService, IDBConnectionService DBConnection) {
			_mailService = mailService;
			_DBConnection = DBConnection;
		}
		// GET: api/<ValuesController>
		[HttpGet]
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}


        [HttpGet("db/{chosenDB}/{SearchParameter}")]
        public async Task<ActionResult<DBSchema>> GetError(string chosenDB, string searchParameter) {

            string baseAddress = "";
            var result = new DBSchema();

            try {
                using (var client = new HttpClient()) {


                    client.BaseAddress = new Uri("http://localhost:9200/" + chosenDB + "/_search");
                    baseAddress = client.BaseAddress.ToString();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("?q=" + searchParameter);

                    if (response.IsSuccessStatusCode) {
                        result = JsonSerializer.Deserialize<DBSchema>(await response.Content.ReadAsStringAsync());
                        return result;
                    } else {
                        throw new HttpRequestException("StatusCode: " + response.StatusCode);
                    }
                }

            } catch (Exception ex) {

                int i = -1;
                i = result.hits.hits.Count();
                result.hits.hits[i]._source.exception = ex.ToString();

                //await PostNewError(result._source);

                var jsonstrings = new String(JsonSerializer.Serialize(searchParameter));
                await _mailService.SendWarningEmailAsync("UpdateIndexWithId", jsonstrings, baseAddress, ex.Message);

                return result;
            }
        }


		[HttpGet("FillDBold/{amount}")]
		public async Task<ActionResult<ResponseStatus>> AbuseThisGenerater(int amount) {

			string baseaddress = "";
			HttpResponseMessage response = new HttpResponseMessage();
			var respStatus = new ResponseStatus();
			IDBInfoGenerater newJsons = new DBInfoGenerater();
			int i = 0;

			try {
				Stopwatch timer = Stopwatch.StartNew();
				var options = new JsonSerializerOptions {
					IgnoreNullValues = true
				};
				while (i < amount) {
					using (var client = new HttpClient()) {
						var jsn = newJsons.GetNewData();

						var jsonstring = new StringContent(JsonSerializer.Serialize(jsn, options), Encoding.UTF8, "application/json");

						client.BaseAddress = new Uri("http://localhost:9200/dbschema/_doc/");

						baseaddress = client.BaseAddress.ToString();
						client.DefaultRequestHeaders.Accept.Clear();
						response = await client.PostAsync("", jsonstring);

						i++;
						//just to see how far we are with generating 
						Debug.WriteLine("added: " + i);
					}

				}
				timer.Stop();
				TimeSpan timeSpan = timer.Elapsed;
				string elaps = String.Format("{0:00}:{1:00}:{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);
				Debug.WriteLine("Done and took: " + elaps);

				if (response.IsSuccessStatusCode) {

					var option = new JsonSerializerOptions {
						Converters = { new DateTimeConverter() }
					};
					respStatus = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync(), option);

					return respStatus;
				} else {
					throw new HttpRequestException("statusCode: " + response.StatusCode);
				}
			} catch (HttpRequestException ex) {
				//await mailService.SendWarningEmailAsync("Post", amount.ToString(), baseaddress, ex.Message);
				return respStatus;
			}
		}


		//Skal kun kaldes igennem en anden GET metode. Er dette nødvendigt at have noget inde i HttpPost med?		
		public async Task<ResponseStatus> PostNewError(DBSchema._Source result) {

			//string baseaddress = "";

			try {

				var options = new JsonSerializerOptions {
					IgnoreNullValues = true
				};

				StringContent jsonstring = new StringContent(JsonSerializer.Serialize(result, options), Encoding.UTF8, "application/json");

				respondStatus = JsonSerializer.Deserialize<ResponseStatus>(await _DBConnection.InsertInToErrorDB(jsonstring), options);
				return respondStatus;

			} catch (HttpRequestException ex) {
				var option = new JsonSerializerOptions {
					IgnoreNullValues = true
				};
				var jsonstrings = new String(JsonSerializer.Serialize(result, option));

				//await mailService.SendWarningEmailAsync("Post", jsonstrings, baseaddress, ex.Message);

				return respondStatus = new ResponseStatus("failed to connect " + ex.Message);
			}
		}


		//public async Task<>

		[HttpPost("dbschema/CheckIfError")]
		public async Task<ResponseStatus> PostCheckIfError([FromBody] DBSchema result) {

			//string baseaddress = "";

			try {

				var option = new JsonSerializerOptions {
					Converters = { new DateTimeConverter() }
				};

				StringContent jsonstring = new StringContent(JsonSerializer.Serialize(result), Encoding.UTF8, "application/json");

				foreach (DBSchema.Hit s in result.hits.hits) {
					foreach (PropertyInfo pi in s._source.GetType().GetProperties()) {
						string value = (string)pi.GetValue(s._source);
						if (pi.Name.Contains("exception") && !string.IsNullOrEmpty(value)) {
							Debug.WriteLine(s);
						}
					}
				}

				respondStatus = JsonSerializer.Deserialize<ResponseStatus>(await _DBConnection.InsertInToMainDB(jsonstring), option);
				return respondStatus;

			} catch (HttpRequestException ex) {

				await PostNewError(result.hits.hits[0]._source);

				return respondStatus = new ResponseStatus("failed to connect " + ex.Message);
			}
		}


		[HttpPost("CheckIfErrorSingle")]
		public async Task<ResponseStatus> PostCheckIfErrorSingleObject([FromBody] DBSchema._Source result) {

			try {

				var options = new JsonSerializerOptions {
					IgnoreNullValues = true
				};

				var jsonstring = new StringContent(JsonSerializer.Serialize(result, options), Encoding.UTF8, "application/json");

				int i = 0;
				while (i < result.GetType().GetProperties().Count()) {
					PropertyInfo pi = result.GetType().GetProperties()[i];
					string value = (string)pi.GetValue(result);
					if (pi.Name.Contains("exception", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(value)) {
						i = result.GetType().GetProperties().Count();
						respondStatus = await PostNewError(result);

						if (respondStatus.result != "created") {
							throw new HttpRequestException();
						}
					}
					i++;
				}

				var option = new JsonSerializerOptions {
					Converters = { new DateTimeConverter() }
				};

				respondStatus = JsonSerializer.Deserialize<ResponseStatus>(await _DBConnection.InsertInToMainDB(jsonstring), option);

				return respondStatus;

			} catch (HttpRequestException ex) {

				//await PostNewError(result);

				return respondStatus = new ResponseStatus(ex.StackTrace);
			}
		}


	}
}
