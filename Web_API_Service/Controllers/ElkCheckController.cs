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
//using static Web_API_Service.Models.DBSchema;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.IO;
using Microsoft.ML;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API_Service.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class ElkCheckController : ControllerBase {

		public readonly IMailService _mailService;
		public readonly IDBConnectionService _DBConnection;

		public ResponseStatus respondStatus = new ResponseStatus();
		public HttpResponseMessage response = new HttpResponseMessage();

		public ElkCheckController(IMailService mailService, IDBConnectionService DBConnection) {
			_mailService = mailService;
			_DBConnection = DBConnection;
		}


		// GET: api/<ValuesController>
		[HttpGet]
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}


		//Robins metode
		[HttpGet("dbschema/getalldb")]
		public async Task<ActionResult<string>> GetDbSchema() {
			DBSchema dbSchema = new DBSchema();
			string commandString = "_search?q=_exists_:\"*exception*\"&sort=timestamp:desc&size=10000&track_scores=true";

			var options = new JsonSerializerOptions	{
				IgnoreNullValues = true,
                Converters = { new DateTimeConverter() }
            };

            string result = await _DBConnection.GetFromMainDBWithQueryString(commandString);
			dbSchema = JsonSerializer.Deserialize<DBSchema>(result, options);

			int index = 0;
			int days = 0;
			var errortime = new Dictionary<string, int>();
			int i = 0;

			//sortér result via timer
			while (i < dbSchema.hits.hits.Length && days < 90000) {
				//tids limit som kan addes til
				
				DateTime timelimit = DateTime.Now.AddDays(-days);

				errortime.Add(timelimit.ToShortDateString(), 0);
				int ii = 0;
				while (i < dbSchema.hits.hits.Length && DateTime.Parse(dbSchema.hits.hits[i]._source.timestamp).ToShortDateString().Contains(timelimit.ToShortDateString())) {
					i++;
					ii++;
				}
				errortime[timelimit.ToShortDateString()] = ii;
				index++;
				days++;
			}

			foreach (var error in errortime) {
				if (error.Value != 0) {
					Debug.WriteLine(error.ToString());
				}
			}
			Debug.WriteLine("Length of hits: " + dbSchema.hits.hits.Length);

			/**
				* Method for creating csv with timestamp and amount of errors per hour
				*/
			//before your loop
			var csv = new StringBuilder();
			string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
			string modelPath = Path.Combine(rootDir, "Data", "ElkTestModel.csv");
			Stopwatch timer = Stopwatch.StartNew();			

			using (var w = new StreamWriter(modelPath))	{


				for (int errorTimeIndex = 0; errorTimeIndex < errortime.Keys.Count(); errorTimeIndex++)	{
					//in your loop
					if (errortime.ElementAt(errorTimeIndex).Value != 0)	{
						var first = errortime.ElementAt(errorTimeIndex).Key.ToString();
						var second = errortime.ElementAt(errorTimeIndex).Value;
						var line = string.Format("{0};{1}", first, second);

						//Suggestion made by KyleMit
						var newLine = string.Format("{0};{1}", first, second);
						//csv.AppendLine(newLine);
						w.WriteLine(line);
						w.Flush();
						Debug.WriteLine("this is for loop run: " + errorTimeIndex);
					}
				}
			}

			timer.Stop();
			TimeSpan timespan = timer.Elapsed;
			string elaps = String.Format("{0:00}:{1:00}:{2:00}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 10);
			Debug.WriteLine("Done and took: " + elaps);
			DateTime t = DateTime.Now;
			Debug.WriteLine("This is current date: " + t.ToShortDateString());

			IMachineLearning check = new MachineLearningService();
			check.CheckForSpikes();



			var option = new JsonSerializerOptions {
				IgnoreNullValues = true
			};

			var jsonstrings = new String(JsonSerializer.Serialize(result, option));
			return jsonstrings;
		}



		// GET api/<ValuesController>/5
		//name need to change to what it does this is just temps
		[HttpGet("project/{error}")]
		public async Task<DBSchema> CheckForError(string error) {
			DBSchema result = new DBSchema();

			long currentTimeInMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			long yesterday = DateTimeOffset.UtcNow.AddHours(-24).ToUnixTimeMilliseconds();

			try {
				string responseString = "";

				if (error.Equals("getall")) {
					responseString = await _DBConnection.GetFromMainDBWithQueryString("_search?q=_exists_:\"*exception*\"&q=timestamp:[" + yesterday.ToString() + "+TO+" + currentTimeInMs.ToString() + "]&size=5&sort=timestamp:desc&track_scores=true");

				} else {
					responseString = await _DBConnection.GetFromMainDBWithQueryString("_search?q=_exists:" + error + "&q=timestamp:[" + yesterday.ToString() + "+TO+" + currentTimeInMs.ToString() + "]&size=5&sort=timestamp:desc&track_scores=true");
				}
				//Forklaring af strengen der står i GetFromEsMainDBWithCommandstring:
				//"?q=" er starten af vores query som fortæller "_search" fra baseAddress hvad den skal lede efter
				//"_exists_" beder "client" om at retunere de objecter som indeholder det søgte streng
				//"\"*exception*\"" eller "error" er det vi søger efter
				//"&q=timestamp:["+ yesterday.ToString() + "+TO+"+ currentTimeInMs.ToString() + "]" er hvor vi beder om kun at få objecter fra et bestemt tidsrum
				//"&size" er hvor mange objecter vi får ud
				//"&sort=timestamp:" sortere vores objecter så vi enten får de ælste først eller de nyeste først
				//"&track_scores=true" er for at forhindre fejl når vi køre metoden


				var option = new JsonSerializerOptions {
					Converters = { new DateTimeConverter() },
					IgnoreNullValues = true
				};

				result = JsonSerializer.Deserialize<DBSchema>(responseString, option);

				return result;

				//using (var client = new HttpClient()) {



				//	if (response.IsSuccessStatusCode) {

				//		var option = new JsonSerializerOptions {
				//			Converters = { new DateTimeConverter() },
				//			IgnoreNullValues = true							
				//		};

				//		result = JsonSerializer.Deserialize<DBSchema>(await response.Content.ReadAsStringAsync(), option);
				//		return result;
				//	} else {
				//		throw new HttpRequestException("statusCode: " + response.StatusCode);
				//	}
				//}
			} catch (HttpRequestException ex) {

				throw ex;
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
		public async Task<object> GetIndexStatus(string index) {
			var result = new clusterHealth("Fail report");
			try {
				string responseString = "";

				if (index.Equals("maindb")) {
					responseString = await _DBConnection.GetHealthFromMainDB();

				} else if (index.Equals("errordb")) {
					responseString = await _DBConnection.GetHealthFromErrorDB();
				}
				result = JsonSerializer.Deserialize<clusterHealth>(responseString);

				if (result.status == ("red")) {
					throw new HttpRequestException();//planen ville være at opsætte en metode, som kontakter den ind i mellem for at checke om den er rød eller ej. 

				}
				return result;

			} catch (HttpRequestException ex) {
				throw ex;
			}
			//try {
			//	string responseString = "";

			//	if (index.Equals("")) {
			//		responseString = await _DBConnection.GetFromMainDBWithQueryStringIndexHealth("_cluster/health");

			//	} else {
			//		//responseString = await _DBConnection.GetFromMainDBWithQueryStringIndexHealth("_stats");
			//		throw new HttpRequestException();

			//	}
			//		result = JsonSerializer.Deserialize<clusterHealth>(responseString);
			//		if (index.Equals("as")) {
			//			result = JsonSerializer.Deserialize<clusterHealth>(responseString);

			//		} else {
			//			var results = new IndexStats();
			//			results = JsonSerializer.Deserialize<IndexStats>(responseString);
			//			return result;
			//		}
			//		if (result.status == ("red"))
			//			return false;

			//		return result;

			//} catch (HttpRequestException ex) {
			//	throw ex;

			//}
		}




		// POST api/<ValuesController>
		[HttpPost("poster")]
		public void PostNewDataEntry([FromBody] DBSchema parametor) {
		}

		//post to dbRecord of all prier post/request types that have been done		
		public void PostToRequestDB([FromBody] string value) {
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

		
		[HttpGet("FillDBold/{amount}")]
		public async Task<ActionResult<ResponseStatus>> AbuseThisGenerater(int amount) {

			string baseaddress = "";
			HttpResponseMessage response = new HttpResponseMessage();
			var respStatus = new ResponseStatus();
			IDBInfoGenerater newJsons = new DBInfoGenerater();
			int i = 0;

			try {
				Stopwatch timer = Stopwatch.StartNew();
				var options = new JsonSerializerOptions
				{
					IgnoreNullValues = true
				};
				while (i < amount)
				{
					using (var client = new HttpClient())
					{
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

		[HttpGet("FillDB/{amount}")]
		public async Task<ResponseStatus> AbuseThisGeneraterShort(int amount) {
			IDBInfoGenerater jsons = new DBInfoGenerater();
			int i = 0;			

			var seOptions = new JsonSerializerOptions {
				IgnoreNullValues = true
			};
			var deOptions = new JsonSerializerOptions {
				IgnoreNullValues = true,
				Converters = { new DateTimeConverter() }
			};

			try {
				
				Stopwatch timer = Stopwatch.StartNew();
				while (i < amount) {

					var jsn = jsons.GetNewData();

					StringContent jsonstring = new StringContent(JsonSerializer.Serialize(jsn, seOptions), Encoding.UTF8, "application/json");
					respondStatus = JsonSerializer.Deserialize<ResponseStatus>(await _DBConnection.InsertInToMainDB(jsonstring), deOptions);

					respondStatus = await PostNewError(jsn);

					i++;
					Debug.WriteLine("added: " + i);
				}
				timer.Stop();
				TimeSpan timespan = timer.Elapsed;
				string elaps = String.Format("{0:00}:{1:00}:{2:00}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds / 10);
				Debug.WriteLine("Done and took: " + elaps);
				return respondStatus;

			} catch (HttpRequestException ex) {

				throw ex;
			}
		}


		[HttpGet("fc")]
		public  void ForecasterTest(int amount) {

			IMachineLearning check = new MachineLearningService();
			check.Forecaster();
		}


	}
}

