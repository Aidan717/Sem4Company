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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Service.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ElkCheckController : ControllerBase {
		// GET: api/<ValuesController>
		[HttpGet]
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}



		// GET api/<ValuesController>/5
		//name need to change to what it does this is just temps
		[HttpGet("project/{error}")]
		public async Task<ActionResult<DBSchemaCopy>> checkForError(string error) {
			var result = new DBSchemaCopy();
			HttpResponseMessage response = new HttpResponseMessage();

			long currentTimeInMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			long yesterday = DateTimeOffset.UtcNow.AddHours(-24).ToUnixTimeMilliseconds();

			try {
				using (var client = new HttpClient()) {

					client.BaseAddress = new Uri("http://localhost:9200/" + "project" + "/_search");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (error.Equals("getAll")) {
						response = await client.GetAsync("?q=_exists_:\"*exception*\"&q=timestamp:["+ yesterday.ToString() + "+TO+"+ currentTimeInMs.ToString() + "]&size=5&sort=timestamp:desc&track_scores=true");

						//Limit delen:
						//&size=5&sort=timestamp:asc

						//limit på 24 timer + add count (total)
						//dernæste limit på antal
					} else {
						response = await client.GetAsync("?q=_exists_:\"*" + error + "*\"&q=timestamp:[" + yesterday.ToString() + "+TO+" + currentTimeInMs.ToString() + "]&size=5&sort=timestamp:desc&track_scores=true");
					}

                    if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							Converters = { new DateTimeConverter() }
						};

						result = JsonSerializer.Deserialize<DBSchemaCopy>(await response.Content.ReadAsStringAsync(), option);
						return result;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}
			} catch (HttpRequestException ex) {

				//return result = new ResponseStatus("failed to connect" + ex.Message);
				return result;
			}

			//Noter til mig selv:
			//kigge hele databasen gennem for alle exception
			//lav noget univercielt
			//er der noget der hedder noget med exception
			//indeholder den noget
			//prøv at match exception med modellen
			//from body er en match
			//Tjekke hele listen i gennem
			//Der er 3 der har en error returner dem
			//Skal have en deafult check og en specifik
			//Limit fejl til 24 timer eller de sidste 50 fejl
			//Connect til kibana som vi gør i testAPI
			//get alle error føst
			//dernæst lav limit
			//udvid langsomt
			
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
		[HttpGet("hey")]
		public async Task<ActionResult<IndexStat>> GetIndexStatus(string index) {
			var result = new IndexStat();
			HttpResponseMessage response = new HttpResponseMessage();

			try {
				using (var client = new HttpClient()) {

					client.BaseAddress = new Uri("http://localhost:9200/" + "_cluster/health/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					response = await client.GetAsync("schools");

					if (response.IsSuccessStatusCode) {

						result = JsonSerializer.Deserialize<IndexStat>(await response.Content.ReadAsStringAsync());
						return result;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}
			}catch(HttpRequestException ex) {

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
        public async Task<ActionResult<DBSchemaCopy>> GetError(string chosenDB, string SearchParameter) {

            string baseaddress = "";
            var result = new DBSchemaCopy();

            try {
                using (var client = new HttpClient()) {


                    client.BaseAddress = new Uri("http://localhost:9200/" + chosenDB + "/_search");
                    baseaddress = client.BaseAddress.ToString();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("?q=" + SearchParameter);

                    if (response.IsSuccessStatusCode) {
                        result = JsonSerializer.Deserialize<DBSchemaCopy>(await response.Content.ReadAsStringAsync());
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

                MailService warningMail = new MailService();

                var jsonstrings = new String(JsonSerializer.Serialize(SearchParameter));
                await warningMail.SendWarningEmailAsync("UpdateIndexWithId", jsonstrings, baseaddress, ex.Message);

                return result;
            }
        }

        //Skal kun kaldes igennem en anden GET metode. Er dette nødvendigt at have noget inde i HttpPost med?
		public async Task<ActionResult<ResponseStatus>> PostNewError([FromBody] DBSchema._Source result) {
			string baseaddress = "";
			HttpResponseMessage response = new HttpResponseMessage();
			var resSta = new ResponseStatus();

			try {

				using (var client = new HttpClient()) {
					var jsonstring = new StringContent(JsonSerializer.Serialize(result), Encoding.UTF8, "application/json");

					client.BaseAddress = new Uri("http://localhost:9200/errordb/_doc/");
					baseaddress = client.BaseAddress.ToString();
					client.DefaultRequestHeaders.Accept.Clear();
					response = await client.PostAsync("", jsonstring);

					if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							Converters = { new DateTimeConverter() }
						};
						resSta = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync());
						return resSta;
					} else {
						throw new HttpRequestException("statusCode: " + response.StatusCode);
					}
				}
			} catch (HttpRequestException ex) {
				MailService warningMail = new MailService();
				var option = new JsonSerializerOptions {
					IgnoreNullValues = true
			};
				var jsonstrings = new String(JsonSerializer.Serialize(result, option));

				await warningMail.SendWarningEmailAsync("Post", jsonstrings, baseaddress, ex.Message);

				return resSta = new ResponseStatus("failed to connect " + ex.Message);
			}
		}

		[HttpPost("{chosenDB}/CheckIfError")]
		public async Task<ActionResult<ResponseStatus>> PostCheckIfError([FromBody] DBSchema._Source result) {
			
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

					if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							Converters = { new DateTimeConverter() }
						};
						respSta = JsonSerializer.Deserialize<ResponseStatus>(await response.Content.ReadAsStringAsync());
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
	}
}

