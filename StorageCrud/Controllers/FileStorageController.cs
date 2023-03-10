using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.AspNetCore.Mvc;
using StorageCrud.repository;
namespace StorageCrud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileStorageController
    {
        [HttpPost("CreateFile")]
        public async Task CreateFile(string fileName)
        {
            await FileStorage.CreateFile(fileName);
        }  
        [HttpPost("CreateDirectory")]
        public async Task CreateDirectory(string directoryName,string fileName)
        {
            await FileStorage.CreateDirectory (directoryName,fileName);
        }  
        [HttpPut("UploadFile")]
        public async Task UploadFile(IFormFile file,string directoryName,string fileShareName)
        {
            await FileStorage.UploadFile(file,directoryName,fileShareName);
        } 
        [HttpDelete("DeleteFileFolder")]
        public async Task DeleteFileFolder(string fileName)
        {
            await FileStorage.DeleteFileFolder(fileName);
        }  
        [HttpDelete("DeleteFile")]
        public async Task DeleteFile(string directoryName,string fileShareName,string fileName)
        {
            await FileStorage.DeleteFile(directoryName,fileShareName,fileName);
        }   
         [HttpDelete("DeleteDirectory")]
        public async Task DeleteDirectory(string directoryName,string fileShareName)
        {
            await FileStorage.DeleteDirectory(directoryName,fileShareName);
        }   
        [HttpGet("GetAllFiles")]
        public async Task<List<string>> GetAllFiles(string directoryName,string fileShareName)
        {
            var data = await FileStorage.GetAllFiles(directoryName,fileShareName);
            return data;
        }   
        [HttpGet("DownloadFile")]
        public async Task DownloadFile(string directoryName,string fileShareName,string fileName)
        {
            await FileStorage.DownloadFiles(directoryName,fileShareName,fileName);
        }   
    }
}