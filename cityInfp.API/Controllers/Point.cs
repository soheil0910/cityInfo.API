using CityInfo.API;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cityInfp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Point : ControllerBase
    {
        // GET: api/<Point>
        [HttpGet]
        public ActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            return Ok(CitiesDataStore.current.Cities);

        }

        // GET api/<Point>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(CitiesDataStore.current.Cities.FirstOrDefault(x=>x.Id==id));
        }


        [HttpGet("{idShahr},{ids}")]
        public ActionResult Get(int idShahr, int ids)
        {
            return Ok(CitiesDataStore.current.Cities.FirstOrDefault(x => x.Id == idShahr).PointsOfInterest.FirstOrDefault(c => c.Id == ids));
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
