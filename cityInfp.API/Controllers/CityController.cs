using CityInfo.API;
using CityInfo.API.Repositoties;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class CityController : ControllerBase
    {
        //private readonly CitiesDataStore _citiesDataStore;
        private readonly ICityInfoRepository _cityInfoRepository;

        public CityController(ICityInfoRepository cityInfoRepository)
        {

            this._cityInfoRepository= cityInfoRepository;
            //_citiesDataStore = citiesDataStore;

        }


        [HttpGet]
        public async Task<ActionResult> GetCity()
        {

            var oor = new JsonResult
                (new List<object> {
               new {   name = "soheil" } ,
               new {   name = "ali" }
            });

            //oor.StatusCode = 404;
            return Ok(oor);
            //return BadRequest(oor);

            return Ok(await _cityInfoRepository.GetCitiesAsync());
            //return Ok(_citiesDataStore.Cities);

        }


        [HttpGet("Async")]
        public async Task<ActionResult> GetCityAsync()
        {
            return await Task.Run(() =>
            {
                return Ok(_cityInfoRepository.GetCitiesAsync());

            });
        }



        //[HttpGet]
        //[HttpPost]
        //public ContentResult GetXml()
        //{
        //    string value = "soheil";
        //    var xml = $"<result><value>{value}</value></result>";
        //    return new ContentResult
        //    {
        //        Content = xml,
        //        ContentType = "application/xml",
        //        StatusCode = 200
        //    };
        //}







        // [HttpGet(template: "Getb")]
        //public JsonResult GetCity1( string b)
        //{
        //    return new JsonResult
        //        (new List<object> {
        //       new {   name = "soheil" } ,
        //       new {   name = "ali" }
        //    });
        //}







    }
}
