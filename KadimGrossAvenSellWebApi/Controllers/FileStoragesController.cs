using Business.Abstract;
using Entity.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileStoragesController : ControllerBase
    {
        private IFileStorageService _fileStorageService;


        public FileStoragesController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        [HttpPost("upload")]
        public IActionResult Upload(FileUploadRequest fileUpload)
        {

            var result = _fileStorageService.AddUri(fileUpload);
            if (result.Success == true)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("download")]
        public IActionResult Download(int id, string collection)
        {

            var result = _fileStorageService.Get(id, collection);
            if (result.Success == true)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
