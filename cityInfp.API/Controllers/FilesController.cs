using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.StaticFiles;
using NuGet.Packaging;
using System.Configuration;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class FilesController : ControllerBase
    {

        //private readonly IConfiguration _configuration;

        public FileExtensionContentTypeProvider fileProvider;
        private readonly string _fileAddress;
        public FilesController(FileExtensionContentTypeProvider fileContentTypeProvider, IConfiguration configuration)
        {
            //_configuration = configuration;

            fileProvider = fileContentTypeProvider;
            _fileAddress = configuration["FileName"];
        }

        [HttpGet]
        public ActionResult GetListFile()
        {
           
            //string[] files = Directory.GetFiles(_configuration["FileName"]);

            string[] files = Directory.GetFiles(_fileAddress);
            return Ok(files.Select(c => c.Substring(6)));
        }

        //[Authorize]
        [HttpGet("{filename}")]
        public ActionResult GetFile(string filename)
        {
           
            string pathToFile = $"{_fileAddress}{filename}";


      



            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            var bytes = System.IO.File.ReadAllBytes(pathToFile);

            if (!fileProvider.TryGetContentType(pathToFile, out var ContentType))
            {
                return Ok("Noe File Shenasaee Nashode");
            }

            return File(bytes, ContentType, Path.GetFileName(pathToFile));
            //return File(bytes, ContentType, "هر اسمی دوست داری");

        }



    }
}



