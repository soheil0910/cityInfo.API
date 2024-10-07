﻿using CityInfo.API;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    //[Route("api/[controller]")]

    


    [Route("api/[controller]/{cityId}/pointsofinterest")]

    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {

        private readonly ILogger<PointsOfInterestController> _logger;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger;
            
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>>
           GetPointsOfInterest(int cityId)
        {
            //try
            //{
            //    throw new Exception("Exeption sample ...");


                var city =
                CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == cityId);


         
           
            if (city == null)
            {
                _logger.LogInformation($"cityId{cityId} is lode");
                _logger.LogDebug("LogDebug");
                _logger.LogWarning("LogWarning");
                _logger.LogCritical("LogCritical");
                _logger.LogError("LogError");
                _logger.LogTrace("LogTrace");
                

                _logger.LogWarning($"cityId{cityId} is not fond");
                return NotFound();
            }

            return Ok(city.PointsOfInterest);


            //}
            //catch (Exception ex)
            //{
            //    _logger.LogCritical($"\n\n\nExeption getting \n {cityId}\n\n \n\n {ex.Message}",ex);
            //    return StatusCode(500, "A Problem happend while ....");
            ////}


        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(
        int cityId, int pointOfInterestId
        )
        {
            var city =
                CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var point = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pointOfInterestId);

            if (point == null)
            {
                return NotFound();
            }

            return Ok(point);
        }
        #region Post

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(
           int cityId,
           PointOfInterestForCreationDto pointOfInterest
           )
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var city = CitiesDataStore.current
                .Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var maxpointOfInterestId = CitiesDataStore.current.Cities
                .SelectMany(c => c.PointsOfInterest)
                .Max(p => p.Id);

            var createPoint = new PointOfInterestDto()
            {
                Id = ++maxpointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(createPoint);



            return CreatedAtAction("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = createPoint.Id

                },
                createPoint
                );
        }
        #endregion

        #region Edit
        [HttpPut("{pontiOfInterestId}")]
        public ActionResult UpdatePointOfInterest(int cityId,
            int pontiOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            //find  city
            var city = CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            // find point of interest
            var point = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pontiOfInterestId);
            if (point == null)
                return NotFound();

            point.Name = pointOfInterest.Name;
            point.Description = pointOfInterest.Description;

            return NoContent();

        }
        #endregion


        #region  Edit with patch
        [HttpPatch("{pontiOfInterestid}")]
        public ActionResult PartiallyUpdatePointOfOnterest(
            int cityId,
            int pontiOfInterestid,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
            )
        {
            //find  city
            var city = CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            // find point of interest
            var pointOfInterestFromStore = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pontiOfInterestid);
            if (pointOfInterestFromStore == null)
                return NotFound();

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(modelState: ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        #endregion


        #region Delete
        [HttpDelete("{pontiOfInterestId}")]
        public ActionResult Delete(int cityId,int pontiOfInterestId)
        {

            //find  city
            var city = CitiesDataStore.current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();

            // find point of interest
            var point = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == pontiOfInterestId);
            if (point == null)
                return NotFound();


            city.PointsOfInterest.Remove(point);

            return NoContent();
        }



        #endregion




    }
}
