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
using MachineLearning;

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
		public async Task<ActionResult<string>> GetAllDb() {
            DBSchema dbSchema = new DBSchema();
            string commandString = "_search?q=_exists_:\"*exception*\"&sort=timestamp:desc&size=10000&track_scores=true";

            var options = new JsonSerializerOptions {
                IgnoreNullValues = true,
                Converters = { new DateTimeConverter() }
            };

            string result = await _DBConnection.GetFromMainDBWithQueryString(commandString);
            dbSchema = JsonSerializer.Deserialize<DBSchema>(result, options);

            Dictionary<string, int> errortime = ErrorCount(dbSchema);
            SaveResultToCSV(errortime);

            IMachineLearning check = new MachineLearningService();
            check.CheckForSpikes();

            var option = new JsonSerializerOptions {
                IgnoreNullValues = true
            };

            var jsonstrings = new String(JsonSerializer.Serialize(result, option));
            return jsonstrings;
        }

        private static void SaveResultToCSV(Dictionary<string, int> errortime) {
            /**
             * Method for creating csv with timestamp and amount of errors per hour
             */
            //before your loop
            var csv = new StringBuilder();
            string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string modelPath = Path.Combine(rootDir, "Data", "ElkTestModel.csv");
            Stopwatch timer = Stopwatch.StartNew();

            using (var w = new StreamWriter(modelPath)) {

				w.WriteLine("date;numError");
                for (int errorTimeIndex = 0; errorTimeIndex < errortime.Keys.Count(); errorTimeIndex++) {
                    //in your loop
                    if (errortime.ElementAt(errorTimeIndex).Value != 0) {
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
        }

        private static Dictionary<string, int> ErrorCount(DBSchema dbSchema) {
            int index = 0;
            int days = 0;
            var errortime = new Dictionary<string, int>();
            int i = 0;

            //sortér result via timer
            while (i < dbSchema.hits.hits.Length && days < 365) {
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
            return errortime;
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
				/*
				 * "?q=" er starten af vores query som fortæller "_search" fra baseAddress hvad den skal lede efter
				 * "_exists_" beder "client" om at retunere de objecter som indeholder det søgte streng
				 * "\"*exception*\"" eller "error" er det vi søger efter
				 * "&q=timestamp:["+ yesterday.ToString() + "+TO+"+ currentTimeInMs.ToString() + "]" er hvor vi beder om kun at få objecter fra et bestemt tidsrum
				 * "&size" er hvor mange objecter vi får ud
				 * "&sort=timestamp:" sortere vores objecter så vi enten får de ælste først eller de nyeste først
				 * "&track_scores=true" er for at forhindre fejl når vi køre metoden
				 */


				var option = new JsonSerializerOptions {
					Converters = { new DateTimeConverter() },
					IgnoreNullValues = true
				};

				result = JsonSerializer.Deserialize<DBSchema>(responseString, option);

                return result;
			} catch (HttpRequestException ex) {
				throw ex;
			}
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

					//respondStatus = await PostNewError(jsn);

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
		public  void ForecasterTest() {

            IMachineLearning check = new MachineLearningService();
            check.Forecaster();
        }

		//Don't run through if classification model doesn't bind to dbschema._source
		[HttpPost("clm")]
		public async Task<ResponseStatus> ClassificationCheck([FromBody]DBSchema._Source classificationSource) {
			
			IMachineLearning machineLearning = new MachineLearningService();

			String mlJsonstring = JsonSerializer.Serialize(classificationSource);
			StringContent jsonContent = new StringContent(JsonSerializer.Serialize(classificationSource), Encoding.UTF8, "application/json");

			Classification dezClassification = JsonSerializer.Deserialize<Classification>(mlJsonstring);

			string str = "";
			foreach (PropertyInfo item in dezClassification.GetType().GetProperties()) {
				str += $"{ (string)item.GetValue(dezClassification) };";
			}
			str = str.Remove(str.Length - 1);
            Debug.WriteLine(str);

            Boolean pass = machineLearning.Classification(str);

            respondStatus = JsonSerializer.Deserialize<ResponseStatus>(await _DBConnection.InsertInToMainDB(jsonContent));

			if (pass) {
                //respondStatus = JsonSerializer.Deserialize<ResponseStatus>(await _DBConnection.InsertInToErrorDB(jsonContent));
                await _DBConnection.InsertInToErrorDB(jsonContent);
            }

			return respondStatus;
		}
	}
}

