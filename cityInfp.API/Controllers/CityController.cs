using CityInfo.API;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class CityController : ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;

        public CityController(CitiesDataStore citiesDataStore)
        {


            _citiesDataStore = citiesDataStore;

        }


        [HttpGet]
        public ActionResult GetCity()
        {

            //var oor= new JsonResult
            //    (new List<object> {
            //   new {   name = "soheil" } ,
            //   new {   name = "ali" }
            //});

            ////oor.StatusCode = 404;
            //return Ok(oor);
            ////return BadRequest(oor);


            return Ok(_citiesDataStore.Cities);

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
