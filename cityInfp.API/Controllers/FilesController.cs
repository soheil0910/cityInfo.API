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

        public FileExtensionContentTypeProvider fileProvider;
        private readonly IConfiguration _configuration;
        public FilesController(FileExtensionContentTypeProvider fileContentTypeProvider, IConfiguration configuration)
        {
            fileProvider = fileContentTypeProvider;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetListFile()
        {
           
            string[] files = Directory.GetFiles(_configuration["FileName:files"]);
            return Ok(files.Select(c => c.Substring(6)));
        }


        [HttpGet("{filename}")]
        public ActionResult GetFile(string filename)
        {
           
            string pathToFile = $"{_configuration["FileName:files"]}{filename}";


      



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

        }



    }
}



