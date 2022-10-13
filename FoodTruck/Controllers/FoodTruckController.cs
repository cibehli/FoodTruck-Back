using FoodTruck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FoodTruck.Controllers
{
    public class FoodTruckController : ApiController
    {
        
        HttpClient client = new HttpClient();
        /// <summary>
        /// Obtiene todos los registros
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> GetAll()
        {
            List<Food> list = new List<Food>();
            client.BaseAddress = new System.Uri("https://data.sfgov.org/resource/jjew-r69b.json?$select=applicant,dayorder,start24,end24,location");
            var response = client.GetAsync("jjew-r69b");
            response.Wait();

            var test = response.Result;
            if (!test.IsSuccessStatusCode)
                return NotFound();
            
                var display = test.Content.ReadAsAsync<List<Food>>();
                
                display.Wait();
                list = display.Result;
                list=list.OrderBy(x => x.applicant).ToList();          
                     
            return Ok(list);
        }
        /// <summary>
        /// Obtiene los registros de acuerdo al dia y la hora
        /// </summary>
        /// <param name="id"></param>
        /// <param name="htime"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetById(int id, string htime)
        {
            List<Food> list = new List<Food>();
            client.BaseAddress = new System.Uri("https://data.sfgov.org/resource/jjew-r69b.json?$select=applicant,dayorder,start24,end24,location");
            var response = client.GetAsync("jjew-r69b");
            response.Wait();
            
            int hora = Convert.ToInt32(htime.Substring(0, 2));
            int min = Convert.ToInt32(htime.Substring(3, 2));

            var test = response.Result;
            if (!test.IsSuccessStatusCode)
                return NotFound();

            var display = test.Content.ReadAsAsync<List<Food>>();

            display.Wait();
            list = display.Result;
            list = list.OrderBy(x => x.applicant).ToList();
            list = list.Where(x => x.dayorder ==id && (hora >=  Convert.ToInt32(x.start24.Substring(0, 2)) && min >= Convert.ToInt32(x.start24.Substring(3, 2))) && (hora <= Convert.ToInt32(x.end24.Substring(0, 2)) && min >= Convert.ToInt32(x.end24.Substring(3, 2)))).ToList();
            return Ok(list);
        }
    }
}