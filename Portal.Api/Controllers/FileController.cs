using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using His.Reception.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Portal.DTO;

namespace Portal.Api.Controllers
{
    public class FileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IFileManagerService _fileManagerService;
        public FileController(IHttpClientFactory httpClientFactory, IConfiguration configuration , IFileManagerService fileManagerService)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _fileManagerService = fileManagerService;
        }

        [HttpGet]
        [Route("[controller]/Download/Image")]
        public async  Task<IActionResult> Download(string fileId ,string width, string height)
        {
            var httpclient = _httpClientFactory.CreateClient();
            httpclient.DefaultRequestHeaders.Add("ApiKey", _configuration["FileManager:Token"]);
            var result =await httpclient.GetAsync($"{_configuration["FileManager:ApiUrl"]}/fm/Getimagefile?width={width}&height={height}&fid={fileId}");

            return File((await result.Content.ReadAsStreamAsync()), result.Content.Headers.ContentType.ToString()); 
        }

        [HttpGet]
        [Route("[controller]/Download")]
        public async Task<IActionResult> DownloadFile(string fileId)
        {
            var result = await _fileManagerService.DownloadFileStream(fileId);

            return File(result.Item1, "application/x-msdownload", result.Item2);
        }

        [HttpPost("[controller]/Upload/Files")]
        public async Task<IActionResult> UploadFiles(MultiUploadFilesDto requestUploadFilesDto)
        {
            var result = await _fileManagerService.UploadFiles(requestUploadFilesDto);

            return Ok();
        }
    }
}
