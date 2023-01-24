using System;
using System.IO;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
namespace StorageCrud.repository
{
    public class BlobStorage
    {
        static string connectionstring = "DefaultEndpointsProtocol=https;AccountName=storageprj301;AccountKey=FkPY8HRBI11ze2fkDkW6Ihwt8ZF66EdgLwJW/V27QlD+sK3BJ3XF+DUdEGf+xYCuji63LFTF4W1m+ASt0Lo5PA==;EndpointSuffix=core.windows.net";
        public static async Task CreateBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentException("enter blob name");
            }
            try{
                BlobContainerClient container = new BlobContainerClient(connectionstring,blobName);
                await container.CreateAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static async Task DeleteBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentException("enter blob name");
            }
            try{
                BlobContainerClient container = new BlobContainerClient(connectionstring,blobName);
                await container.DeleteAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static async Task DeleteBlobContent(string blobName,string file)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentException("enter blob name");
            }
            try{
                BlobContainerClient container = new BlobContainerClient(connectionstring,blobName);
                await container.DeleteBlobAsync(file);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public static async Task<BlobProperties> UpdateBlobContent(string blobName,string file)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentException("enter blob name");
            }
            try{
                string fileName = Path.GetTempFileName();
                BlobContainerClient container = new BlobContainerClient(connectionstring,blobName);
                BlobClient blob = container.GetBlobClient(file);
                await blob.UploadAsync(fileName);
                BlobProperties prop = await blob.GetPropertiesAsync();
                return prop;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public static async Task<BlobProperties> GetBlobContent(string blobName,string file)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentException("enter blob name");
            }
            try{
                BlobContainerClient container = new BlobContainerClient(connectionstring,blobName);
                BlobClient blob = container.GetBlobClient(file);
                BlobProperties prop = await blob.GetPropertiesAsync();
                return prop;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public static async Task<List<string>> GetBlob(string blobName,string file)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentException("enter blob name");
            }
            try{
                BlobContainerClient container = new BlobContainerClient(connectionstring,blobName);
                List<string> names = new List<string>();
                await foreach(BlobItem a in container.GetBlobsAsync())
                {
                    names.Add(a.Name);
                }
                return names;
                
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public static async Task<BlobProperties> DownloadBlob(string blobName,string file)
        {
            try{
                string Path = @"C:\Users\vmadmin\Desktop\StorageCrud\StorageCrud\download\"+blobName;
                BlobContainerClient container = new BlobContainerClient(connectionstring,blobName);
                BlobClient client = container.GetBlobClient(file);
                await client.DownloadToAsync(Path);
                BlobProperties prop = await client.GetPropertiesAsync();
                return prop;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}