using MeterReadingsApi.Services.UploadServices.Interfaces;
using MeterReadingsApi.Services.UploadServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeterReadingsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingsController : ControllerBase
    {
        private readonly IUploadMeterReadingsService _uploadService;

        public MeterReadingsController(IUploadMeterReadingsService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public Task<UploadMeterReadingsResultsModel> Post(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var csvData = reader.ReadToEnd();

                return _uploadService.ProcessUpload(csvData);
            }
        }
    }
}
