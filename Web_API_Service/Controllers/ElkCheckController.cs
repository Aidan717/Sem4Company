using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_API_Service.Models;
using System.Text;
using Web_API_Service.Service;
using System.Text.Json;

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
        [HttpGet("{error}")]
        //public async Task<ActionResult<DBSchema>> breh(string index) {

        //    using (var client = new HttpClient()) {

        //    }

        //    return "value";
        //}

        //look up something specific with in the last hour that have given an exception or something
        [HttpGet("{error}")]
		public string LookUpSomeThingSpecific() {
			return "value";
		}

        //look at status for index when a person call something
        [HttpGet("db/{index}")]
        public async Task<ActionResult<IndexStat>> GetIndexStatus() {

            using (var client = new HttpClient()) {
                var result = new IndexStat();
                client.BaseAddress = new Uri("http://localhost:9200/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               


            }
        }


        //http://localhost:9200/_cat/indices?h=index laver en en liste af alle indexes
        //http://localhost:9200/_cluster/health?level=indices henter alle indexes med en oversigt heriblandt status.




        //look up history of a index and see if something does not fit in
        [HttpGet("{error}")]
		public string LookUphistory() {
			return "value";
		}


		// POST api/<ValuesController>
		[HttpPost]
		public void PostNewDataEntry([FromBody] string value) {
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
