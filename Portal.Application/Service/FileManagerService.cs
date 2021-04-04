using His.Reception.Application.Interface;
using His.Reception.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Portal.Context;
using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class FileManagerService : IFileManagerService
    {
        private readonly IConfiguration _configuration;
        private readonly IFilesService _filesService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DbSet<FileGroup> _repositoryFileGroup;

        public FileManagerService(IConfiguration configuration, IFilesService filesService, IStringLocalizer<SharedResource> sharedLocalizer,
            IHttpClientFactory httpClientFactory, IWorkContextService workContext, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _filesService = filesService;
            _httpClientFactory = httpClientFactory;
            _repositoryFileGroup = unitOfWork.Set<FileGroup>();
        }

        private async Task<FileManagerDto> GetFileApi(List<string> fIds)
        {
            var url = _configuration["FileManager:ApiUrl"];
            var path = $"{url}/fm/api/file/infos";

            var body = JsonConvert.SerializeObject(new
            {
                ApiKey = _configuration["FileManager:Token"],
                Culture = "en-US",
                FIDList = fIds
            });

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(path),
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FileManagerDto>(responseBody);

            return result;
        }

        public async Task<FileManagerDto> GetFiles(List<string> fIds)=> await GetFileApi(fIds);
     

        public async Task<FileManagerDataDto> GetById(string id)
        {
            var ids = new List<string>
            {
                id
            };

            var result = await GetFileApi(ids);

            return result.Data?.FirstOrDefault();
        }

        public async Task<byte[]> Download(string id)
        {
            var register = $"{_configuration["FileManager:ApiUrl"]}/fm/api/download/file";

            // HttpClient client = new HttpClient();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["FileManager:Token"]);
            client.DefaultRequestHeaders.Add("FID", id);

            //var content = new StringContent( (new { PrimeryKey=id }).ToString(), Encoding.UTF8, "application/json");
            //var res1 = await client.PostAsync(getFile, content);
            //var file = res1.Content.ReadAsByteArrayAsync().Result;

            var res0 = await client.PostAsync(register, null);
            var fileBytes = await res0.Content.ReadAsByteArrayAsync();
            return fileBytes;
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }

        public async Task<(byte[], string)> DownloadFileStream(string id)
        {
            var register = $"{_configuration["FileManager:ApiUrl"]}/fm/api/download/file";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["FileManager:Token"]);
            client.DefaultRequestHeaders.Add("FID", id);

            //var content = new StringContent( (new { PrimeryKey=id }).ToString(), Encoding.UTF8, "application/json");
            //var res1 = await client.PostAsync(getFile, content);
            //var file = res1.Content.ReadAsByteArrayAsync().Result;

            var res0 = await client.PostAsync(register, null);

            var fileBytes = await res0.Content.ReadAsByteArrayAsync();
            //return fileBytes;

            return (fileBytes, res0.Content.Headers.ContentDisposition.FileName.ToString());
        }

        public async Task<string> GetImageAsBase64(string id)
        {
            var register = $"{_configuration["FileManager:ApiUrl"]}/fm/api/download/file";

            // HttpClient client = new HttpClient();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["FileManager:Token"]);
            client.DefaultRequestHeaders.Add("FID", id);

            var res0 = await client.PostAsync(register, null);
            var byt = await res0.Content.ReadAsByteArrayAsync();
            var base64 = Convert.ToBase64String(byt);

            return "data:image/png;base64, " + base64;
        }

        public async Task<string> SendSmsUpload(FileUploadDto files)
        {
            var filename = files.File.FileName;
            var fileId = string.Empty;

            //  var client = new HttpClient();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["FileManager:Token"]);
            client.DefaultRequestHeaders.Add("CategoryID", files.CategoryID);
            client.DefaultRequestHeaders.Add("Description", files.Description);
            client.DefaultRequestHeaders.Add("MetaData", files.MetaData);
            client.DefaultRequestHeaders.Add("Culture", files.Culture);
            client.BaseAddress = new Uri(_configuration["FileManager:ApiUrl"]);

            // var documentToSend = System.IO.File.ReadAllBytes(path);
            var multipartFormDataContent = new MultipartFormDataContent();

            //multipartFormDataContent.Add(new ByteArrayContent(documentToSend), "File", filename);
            using (var memoryStream = new MemoryStream())
            {
                await files.File.CopyToAsync(memoryStream);
                multipartFormDataContent.Add(new ByteArrayContent(memoryStream.ToArray()), "File", filename);
            }

            var result = await client.PostAsync("/fm/api/upload/file", multipartFormDataContent);
            if (result.IsSuccessStatusCode)
            {
                var res = result.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<UploadFileResponseDto>(res.Result);
                if (account.Data.UploadStatus == 0)
                {
                    //return account.Data.FID.ToString();
                    var file = new FilesDto
                    {
                        FileGroupId = files.FileGroupId,
                        PrimeryKey = files.PrimeryKey,
                        TableName = files.TableName,
                        FileName = filename,
                        RefferKey = account.Data.FID.ToString()


                    };

                    fileId = account.Data.FID.ToString();

                    await _filesService.AddAsync(file);
                    return fileId;
                }
            }

            return fileId;
        }

        public async Task<string> Upload(FileUploadDto files)
        {
            string filename = files.File.FileName;
            string fileId = string.Empty;

            //  var client = new HttpClient();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["FileManager:Token"]);
            client.DefaultRequestHeaders.Add("CategoryID", files.CategoryID);
            client.DefaultRequestHeaders.Add("Description", files.Description);
            client.DefaultRequestHeaders.Add("MetaData", files.MetaData);
            client.DefaultRequestHeaders.Add("Culture", files.Culture);
            client.BaseAddress = new Uri(_configuration["FileManager:ApiUrl"]);

            // var documentToSend = System.IO.File.ReadAllBytes(path);
            var multipartFormDataContent = new MultipartFormDataContent();

            //multipartFormDataContent.Add(new ByteArrayContent(documentToSend), "File", filename);
            using (var memoryStream = new MemoryStream())
            {
                await files.File.CopyToAsync(memoryStream);
                multipartFormDataContent.Add(new ByteArrayContent(memoryStream.ToArray()), "File", filename);
            }

            var result = await client.PostAsync("/fm/api/upload/file", multipartFormDataContent);
            if (result.IsSuccessStatusCode)
            {
                var res = result.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<UploadFileResponseDto>(res.Result);
                if (account.Data.UploadStatus == 0)
                {
                    //return account.Data.FID.ToString();
                    var file = new FilesDto
                    {
                        FileGroupId = files.FileGroupId,
                        PrimeryKey = files.PrimeryKey,
                        TableName = files.TableName,
                        FileName = filename,
                        RefferKey = account.Data.FID.ToString()
                    };

                    fileId = account.Data.FID.ToString();

                    await _filesService.AddAsync(file);
                    return fileId;
                }
            }

            return fileId;
        }

        public async Task<string> UploadFiles(MultiUploadFilesDto files)
        {
            var fileId = string.Empty;
            var fileTag = new List<FileTagDto>();

            if (!string.IsNullOrEmpty(files.Tags)) fileTag = JsonConvert.DeserializeObject<List<FileTagDto>>(files.Tags);
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("ApiKey", _configuration["FileManager:Token"]);
            client.DefaultRequestHeaders.Add("CategoryID", files.CategoryID);
            client.DefaultRequestHeaders.Add("Description", files.Description);
            client.DefaultRequestHeaders.Add("MetaData", files.MetaData);
            client.DefaultRequestHeaders.Add("Culture", files.Culture);
            client.BaseAddress = new Uri(_configuration["FileManager:ApiUrl"]);

            var multipartFormDataContent = new MultipartFormDataContent();
            foreach (var file in files.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    multipartFormDataContent.Add(new ByteArrayContent(memoryStream.ToArray()), "Files", file.FileName);
                }
            }

            var fileGroupId = _repositoryFileGroup.Where(d => d.Code == files.FileGroupCode).Select(g => g.Id).FirstOrDefault();

            var result = await client.PostAsync("/fm/api/upload/files", multipartFormDataContent);
            if (result.IsSuccessStatusCode)
            {
                var res = result.Content.ReadAsStringAsync();
                var uploadsResult = JsonConvert.DeserializeObject<UploadFilesResponseDto>(res.Result);

                int i = 0;
                var fileDtos = new List<FileAndTagUploadDto>();
                foreach (var item in uploadsResult.Data)
                    if (item.UploadStatus == 0)
                    {
                        var file = new FileAndTagUploadDto
                        {
                            FileGroupId = fileGroupId,
                            PrimeryKey = files.PrimeryKey,
                            TableName = files.TableName,
                            FileName = files.Files[i].FileName,
                            RefferKey = item.FID.ToString(),
                            Tags = fileTag.Where(d => d.FileName == files.Files[i].FileName).SelectMany(g => g.Tags).ToList(),
                            CreateDate = item.CreateDate ?? DateTime.Now.ToDateTimeString()
                        };
                        i++;
                        fileId = 0.ToString();
                        fileDtos.Add(file);
                    }

                await _filesService.AddTagsAsync(fileDtos);
            }

            return fileId;
        }

        public Task<string> UploadFiles(FileUploadDto files)=>throw new NotImplementedException();
    }
}
