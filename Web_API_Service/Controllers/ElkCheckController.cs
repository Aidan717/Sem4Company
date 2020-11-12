using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_API_Service.Models;
using Web_API_Service.Utility;
using Web_API_Service.Service;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Diagnostics;
using static Web_API_Service.Models.DBSchema;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Service.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ElkCheckController : ControllerBase {

		public readonly IMailService mailService;
		public ElkCheckController(IMailService mailService) {
			this.mailService = mailService;
		}


		// GET: api/<ValuesController>
		[HttpGet]
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}


		//Robins metode
		[HttpGet("dbschema/getall")]
		public async Task<ActionResult<string>> GetDbSchema() {

			using (var client = new HttpClient()) {
				var result = new DBSchema();
				client.BaseAddress = new Uri("http://localhost:9200/dbschema/_search");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await client.GetAsync("?q=_exists_:\"*exception*\"&sort=timestamp:desc&track_scores=true");

				if (response.IsSuccessStatusCode) {

                    var options = new JsonSerializerOptions {
                        Converters = { new DateTimeConverter() }
                    };


                    result = JsonSerializer.Deserialize<DBSchema>(await response.Content.ReadAsStringAsync(), options);

					int index = 0;
					int hour = 1;
					var errortime = new Dictionary<DateTime, int>();
					int i = 0;
				

					//sortér result via timer
					while (i < result.hits.hits.Length && hour < 730) {
						//tids limit som kan addes til
						DateTime timelimit = DateTime.Now.AddHours(-hour);
						errortime.Add(timelimit, 0);
						while (i < result.hits.hits.Length && DateTime.Parse(result.hits.hits[i]._source.timestamp) > timelimit) {
							errortime[timelimit] += 1;
							i++;
						}
						index++;
						hour++;
					}
					Debug.WriteLine("[2 3[");
					foreach (var error in errortime) {
						if (error.Value != 0) {
							Debug.WriteLine(error.ToString());
						}
					}
					var option = new JsonSerializerOptions {
						IgnoreNullValues = true
					};

					var jsonstrings = new String(JsonSerializer.Serialize(result, option));
					return jsonstrings;
				} else {
					return result.ToString();
				}
			}
		}



		// GET api/<ValuesController>/5
		//name need to change to what it does this is just temps
		[HttpGet("project/{error}")]
		public async Task<ActionResult<DBSchema>> checkForError(string error) {
			var result = new DBSchema();
			HttpResponseMessage response = new HttpResponseMessage();

			long currentTimeInMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			long yesterday = DateTimeOffset.UtcNow.AddHours(-24).ToUnixTimeMilliseconds();

			try {
				using (var client = new HttpClient()) {

					client.BaseAddress = new Uri("http://localhost:9200/dbschema/_search");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (error.Equals("getall")) {
						response = await client.GetAsync("?q=_exists_:\"*exception*\"&q=timestamp:["+ yesterday.ToString() + "+TO+"+ currentTimeInMs.ToString() + "]&size=5&sort=timestamp:desc&track_scores=true");

					} else {
						response = await client.GetAsync("?q=_exists_:\"*" + error + "*\"&q=timestamp:[" + yesterday.ToString() + "+TO+" + currentTimeInMs.ToString() + "]&size=5&sort=timestamp:desc&track_scores=true");
					}
					
					//Forklaring af strengen der står i getAsync:
					//"?q=" er starten af vores query som fortæller "_search" fra baseAddress hvad den skal lede efter
					//"_exists_" beder "client" om at retunere de objecter som indeholder det søgte streng
					//"\"*exception*\"" eller "error" er det vi søger efter
					//"&q=timestamp:["+ yesterday.ToString() + "+TO+"+ currentTimeInMs.ToString() + "]" er hvor vi beder om kun at få objecter fra et bestemt tidsrum
					//"&size" er hvor mange objecter vi får ud
					//"&sort=timestamp:" sortere vores objecter så vi enten får de ælste først eller de nyeste først
					//"&track_scores=true" er for at forhindre fejl når vi køre metoden

					if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							Converters = { new DateTimeConverter() },
							IgnoreNullValues = true							
						};

						result = JsonSerializer.Deserialize<DBSchema>(await response.Content.ReadAsStringAsync(), option);
						return result;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}
			} catch (HttpRequestException ex) {

				return result;
			}
		}

		//look up something specific with in the last hour that have given an exception or something
		[HttpGet("{error}")]
		public string LookUpSomeThingSpecific() {
			return "value";
		}

		//look at status for index when a person call something
		[HttpGet("{error}")]
		public string LookforIndexStatus() {
			return "value";
		}

		//look at status for index when a person call something
		[HttpGet("hey/{index}")]
		public async Task<ActionResult<object>> GetIndexStatus(string index) {
			var result = new clusterHealth("Fail report");
			HttpResponseMessage response = new HttpResponseMessage();

			try {
				using (var client = new HttpClient()) {

					client.BaseAddress = new Uri("http://localhost:9200/" + "_cluster/health/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					if (index.Equals("as")) {
						response = await client.GetAsync("/_cluster/health/schools");

					} else {
						response = await client.GetAsync("schools/_stats");

					}
					if (response.IsSuccessStatusCode) {

						result = JsonSerializer.Deserialize<clusterHealth>(await response.Content.ReadAsStringAsync());
						if (index.Equals("as")) {
							result = JsonSerializer.Deserialize<clusterHealth>(await response.Content.ReadAsStringAsync());

						} else {
							var results = new IndexStats();
							results = JsonSerializer.Deserialize<IndexStats>(await response.Content.ReadAsStringAsync());
							return result;
						}
						if (result.status == ("red"))
							return result.status;

						return result;

					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}
			} catch (HttpRequestException ex) {
				return result;

			}

		}


		//http://localhost:9200/_cat/indices?h=index laver en en liste af alle indexes
		//http://localhost:9200/_cluster/health?level=indices henter alle indexes med en oversigt heriblandt status.





		// POST api/<ValuesController>
		[HttpPost("poster")]
		public void PostNewDataEntry([FromBody] DBSchema parametor) {
		}

		//post to dbRecord of all prier post/request types that have been done		
		public void PostToRequestDB([FromBody] string value) {
		}



        [HttpGet("db/{chosenDB}/{SearchParameter}")]
        public async Task<ActionResult<DBSchema>> GetError(string chosenDB, string SearchParameter) {

            string baseaddress = "";
            var result = new DBSchema();

            try {
                using (var client = new HttpClient()) {


                    client.BaseAddress = new Uri("http://localhost:9200/" + chosenDB + "/_search");
                    baseaddress = client.BaseAddress.ToString();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("?q=" + SearchParameter);

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

                

                var jsonstrings = new String(JsonSerializer.Serialize(SearchParameter));
                await mailService.SendWarningEmailAsync("UpdateIndexWithId", jsonstrings, baseaddress, ex.Message);

                return result;
            }
        }

        //Skal kun kaldes igennem en anden GET metode. Er dette nødvendigt at have noget inde i HttpPost med?
		public async Task<ActionResult<ResponseStatus>> PostNewError(DBSchema._Source result) {
			string baseaddress = "";
			HttpResponseMessage response = new HttpResponseMessage();
			var resSta = new ResponseStatus();
			
			try {

				using (var client = new HttpClient()) {

					var options = new JsonSerializerOptions {
						IgnoreNullValues = true
					};

					var jsonstring = new StringContent(JsonSerializer.Serialize(result, options), Encoding.UTF8, "application/json");

					client.BaseAddress = new Uri("http://localhost:9200/errordb/_doc/");
					baseaddress = client.BaseAddress.ToString();
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.PostAsync("", jsonstring);

					if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							Converters = { new DateTimeConverter() }
						};
						resSta = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync(), option);
						return resSta;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}
			} catch (HttpRequestException ex) {
				var option = new JsonSerializerOptions {
					IgnoreNullValues = true
				};
				var jsonstrings = new String(JsonSerializer.Serialize(result, option));

				await mailService.SendWarningEmailAsync("Post", jsonstrings, baseaddress, ex.Message);

				return resSta = new ResponseStatus("failed to connect " + ex.Message);
			}
		}

		[HttpPost("dbschema/CheckIfError")]
		public async Task<ActionResult<ResponseStatus>> PostCheckIfError([FromBody] DBSchema result) {
			
			string baseaddress = "";
			HttpResponseMessage response = new HttpResponseMessage();
			var respSta = new ResponseStatus();
			
			try {

				using (var client = new HttpClient()) {
					var jsonstring = new StringContent(JsonSerializer.Serialize(result), Encoding.UTF8, "application/json");

					client.BaseAddress = new Uri("http://localhost:9200/dbschema/_doc/");
					baseaddress = client.BaseAddress.ToString();
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.PostAsync("", jsonstring);

					
					var errorAdd = new DBSchema();

					Debug.WriteLine(result.ToString());

					int i = 0;
					foreach (Hit s in result.hits.hits) {
						foreach (PropertyInfo pi in s._source.GetType().GetProperties()) {
							string value = (string)pi.GetValue(s._source);
							if (pi.Name.Contains("exception") && !string.IsNullOrEmpty(value)) {
								Debug.WriteLine(s);
							}
						}
					}

					if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							Converters = { new DateTimeConverter() }
						};
						respSta = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync(), option);
						return respSta;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}
			} catch (HttpRequestException ex) {

				await PostNewError(result.hits.hits[0]._source);

				return respSta;
			}
		}


		[HttpPost("CheckIfErrorSingle")]
		public async Task<ActionResult<ResponseStatus>> PostCheckIfErrorSingleObject([FromBody] DBSchema._Source result) {

			string baseaddress = "";
			HttpResponseMessage response = new HttpResponseMessage();
			var respSta = new ResponseStatus();

			try {

				using (var client = new HttpClient()) {

					var options = new JsonSerializerOptions {
						IgnoreNullValues = true
					};

					var jsonstring = new StringContent(JsonSerializer.Serialize(result, options), Encoding.UTF8, "application/json");

					client.BaseAddress = new Uri("http://localhost:9200/dbschema/_doc/");
					baseaddress = client.BaseAddress.ToString();
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.PostAsync("", jsonstring);



					int i = 0;
					while (i < result.GetType().GetProperties().Count()) {
						PropertyInfo pi = result.GetType().GetProperties()[i];
							string value = (string)pi.GetValue(result);
							if (pi.Name.Contains("exception", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(value)) {
								i = result.GetType().GetProperties().Count();

							await PostNewError(result);

						}
						i++;
					}

                    if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							Converters = { new DateTimeConverter() }
						};
						respSta = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync(), option);

                        return respSta;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}
			} catch (HttpRequestException ex) {

				await PostNewError(result);

				return respSta;
			}
		}






		// PUT api/<ValuesController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value) {
		}








		// DELETE api/<ValuesController>/5
		[HttpDelete("{id}")]
		public void Delete(int id) {
		}





		//[HttpPost("ad")]
		//public async Task getser(int id) {
		//	var newjsons = new DBInfoGenerater();
		//	await AbuseThis(newjsons);
		//}

		
		//[HttpGet("FillDB/{amount}")]
		public async Task<ActionResult<ResponseStatus>> AbuseThisGenerater(int amount) {

			var respStatus = new ResponseStatus();
			IDBInfoGenerater newjsons = new DBInfoGenerater();
			int i = 0;
			
			try {

				
				Stopwatch timer = Stopwatch.StartNew();
				while (i < amount) {
					var jsn = newjsons.getNewData();
					await PostCheckIfErrorSingleObject(jsn);
					i++;
					//just to see how far we are with generating 
					Debug.WriteLine("added: " + i);
				}
				timer.Stop();
				TimeSpan timespan = timer.Elapsed;
				string elaps = String.Format("{0:00}:{1:00}:{2:00}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 10);
				Debug.WriteLine("Done and took: " + elaps);

				return respStatus;
				
			} catch (HttpRequestException ex) {
				//await mailService.SendWarningEmailAsync("Post", amount.ToString(), baseaddress, ex.Message);
				return respStatus;
			}
		}




	}
}

