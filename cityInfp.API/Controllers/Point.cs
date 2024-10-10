using CityInfo.API;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Point : ControllerBase
    {

        private readonly CitiesDataStore _citiesDataStore;

        public Point(CitiesDataStore citiesDataStore)
        {


            _citiesDataStore = citiesDataStore;

        }










        // GET: api/<Point>
        [HttpGet]
        public ActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            return Ok(_citiesDataStore.Cities);

        }

        // GET api/<Point>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_citiesDataStore.Cities.FirstOrDefault(x=>x.Id==id));
        }


        [HttpGet("{idShahr},{ids}")]
        public ActionResult Get(int idShahr, int ids)
        {
            return Ok(_citiesDataStore.Cities.FirstOrDefault(x => x.Id == idShahr).PointsOfInterest.FirstOrDefault(c => c.Id == ids));
        }




        //// POST api/<Point>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<Point>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<Point>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
