using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Web_API_Service.Models;

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

			try {
				using (var client = new HttpClient()) {

					client.BaseAddress = new Uri("http://localhost:9200/" + "project" + "/_search");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					response = await client.GetAsync("?q=_exists_:" + error /*"activities.exceptions.errors.message"*/);

					if (response.IsSuccessStatusCode) {

						var option = new JsonSerializerOptions {
							//PropertyNameCaseInsensitive = false
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

		//look up history of a index and see if something does not fit in
		[HttpGet("{error}")]
		public string LookUphistory() {
			return "value";
		}










		// POST api/<ValuesController>
		[HttpPost("poster")]
		public void PostNewDataEntry([FromBody] DBSchema parametor) {
		}

		//post to dbRecord of all prier post/request types that have been done		
		public void PostToRequestDB([FromBody] string value) {
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
