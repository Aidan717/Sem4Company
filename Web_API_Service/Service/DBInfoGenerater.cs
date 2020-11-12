using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Web_API_Service.Models;

namespace Web_API_Service.Service {
	public class DBInfoGenerater : IDBInfoGenerater {
		Random numGenenerater = new Random();


		public DBSchema._Source getNewData() {
			

			int i = 0;
			//while (i < amount) { }
			DBSchema._Source data = new DBSchema._Source();
			
			//should it generate errors of not
			if(numGenenerater.Next(0,100)< 33) {
				setNewError(data);
			}


			data.name = getName();
			data.timestamp = getRandomDate();


			
			return data;
		}



		public void setNewError(DBSchema._Source data) {
			int numberOFerrors = numGenenerater.Next(1,5);
			int ii = 0;
			while (ii < numberOFerrors) {
				int i = numGenenerater.Next(1, 5);
				switch (i) {
					case 1:
						data.activitiesexceptionsinnerExceptionmessage = "something went wrong along planet 9";					
					break;
					case 2:
						data.activitiesexceptionsmessage = "it just dont work to day";					
					break;
					case 3:
						data.exceptiondetailmessage = "we was implementing error for errors sake";
					break;
					case 4:
						data.activitiesexceptionsinnerExceptionnativeErrorCode = "12356443";
					break;
					case 5:
						data.activitiestype = "sql";
					break;
				}
				ii++;
			}
		}
		public string getName() {
			return new Utility.UserNameList().Name;			
		}

		public string getRandomDate() {
			DateTime date = new DateTime(2010, 1, 1);
			DateTime test = new DateTime(2015, 12, 12);
			int range = (DateTime.Today - date).Days;
			return date.AddDays(numGenenerater.Next(range)).AddHours(numGenenerater.Next(0, 24)).AddMinutes(numGenenerater.Next(0, 60)).AddSeconds(numGenenerater.Next(0, 60)).ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss");

		}

	}
}

