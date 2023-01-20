using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using StorageCrud.repository;
namespace StorageCrud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlobsController
    {
       
        [HttpPost("AddBlob")]
        public async Task<string> AddBlob(string blobName)
        {
            await repository.BlobStorage.CreateBlob(blobName);
            return null;
        }
        [HttpDelete("DeleteBlob")]
        public async Task<string> DeleteBlob(string blobName)
        {
            await repository.BlobStorage.DeleteBlob(blobName);
            return null;
        }
        [HttpDelete("DeleteBlobContent")]
        public async Task<string> DeleteBlobContent(string blobName,string file)
        {
            await repository.BlobStorage.DeleteBlobContent(blobName,file);
            return null;
        }
        [HttpPut("UpdateBlobContent")]
        public async Task<string> UpdateBlobContent(string blobName,string file)
        {
            await repository.BlobStorage.UpdateBlobContent(blobName,file);
            return null;
        }
        [HttpGet("GetBlobContent")]
        public async Task<BlobProperties> GetBlobContent(string blobName,string file)
        {
            var data = await repository.BlobStorage.GetBlobContent(blobName,file);
            return data;
        }
        [HttpGet("GetBlob")]
        public async Task<List<string>> GetBlob(string blobName,string file)
        {
            var data = await repository.BlobStorage.GetBlob(blobName,file);
            return data;
        }
        [HttpGet("DownloadBlobContent")]
        public async Task<BlobProperties> DownloadBlobContent(string blobName,string file)
        {
            var data = await repository.BlobStorage.DownloadBlob(blobName,file);
            return data;
        }
    }
}