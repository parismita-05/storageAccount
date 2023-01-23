using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
namespace StorageCrud.repository
{
    public class FileStorage
    {
        static string connectionstring ="DefaultEndpointsProtocol=https;AccountName=storageprj301;AccountKey=GArak/zF1uGFlKOV5II4RPds9QsB80Y4o6xrGKsGYWvHJ+zB4HVaw24kKJ294eXQN2XM4gfy/d/U+AStBeyqDA==;EndpointSuffix=core.windows.net";
        static ShareServiceClient shareServiceClient;
        public static async Task CreateFile(string fileName)
        {
            try{
                shareServiceClient = new ShareServiceClient(connectionstring);
                var serviceClient = shareServiceClient.GetShareClient(fileName);
                await serviceClient.CreateIfNotExistsAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
            
            public static async Task CreateDirectory(string directoryName,string fileName)
            {
                try{
                shareServiceClient = new ShareServiceClient(connectionstring);
                var serviceClient = shareServiceClient.GetShareClient(fileName);
                ShareDirectoryClient rootDir = serviceClient.GetRootDirectoryClient();
                ShareDirectoryClient fileDir = rootDir.GetSubdirectoryClient(directoryName);
                await fileDir.CreateIfNotExistsAsync();
            }  
                catch(Exception e)
                {
                    throw e;
                } 
            }
            public static async Task DeleteDirectory(string directoryName,string fileShareName)
            {
                shareServiceClient = new ShareServiceClient(connectionstring);
                var serviceClient = shareServiceClient.GetShareClient(fileShareName);
                var dir = serviceClient.GetDirectoryClient(directoryName);
                await dir.DeleteAsync();   
            }
             public static async Task DeleteFile(string directoryName,string fileShareName,string fileName)
            {
                shareServiceClient = new ShareServiceClient(connectionstring);
                var serviceClient = shareServiceClient.GetShareClient(fileShareName);
                var dir = serviceClient.GetDirectoryClient(directoryName);
                var file = dir.GetFileClient(fileName);
                await file.DeleteAsync();   
            }
             public static async Task<List<string>> GetAllFiles(string directoryName,string fileShareName)
            {
                shareServiceClient = new ShareServiceClient(connectionstring);
                var serviceClient = shareServiceClient.GetShareClient(fileShareName);
                var files = serviceClient.GetRootDirectoryClient();
                var Directory = serviceClient.GetDirectoryClient(directoryName);
                List<string> name = new List<string>();
                await foreach(ShareFileItem file in Directory.GetFilesAndDirectoriesAsync())
                {
                    name.Add(file.Name);
                }   
                return name;
            }
            public static async Task DownloadFiles(string directoryName,string fileShareName,string fileName)
            {
                string path = @"C:\Users\vmadmin\Desktop\storageAcc\StorageCrud\download\"+fileName;
                shareServiceClient = new ShareServiceClient(connectionstring);
                var serviceClient = shareServiceClient.GetShareClient(fileShareName);
                var dir = serviceClient.GetDirectoryClient(directoryName);
                var file = dir.GetFileClient(fileName);
                ShareFileDownloadInfo download = await file.DownloadAsync();
                using(FileStream stream=File.OpenWrite(path))
                {
                    await download.Content.CopyToAsync(stream);
                }

            }
            public static async Task UploadFile(IFormFile file,string directoryName,string fileShareName)
            {
                string fileName = file.FileName;
                shareServiceClient = new ShareServiceClient(connectionstring);
                var serviceClient = shareServiceClient.GetShareClient(fileShareName);
                var directory = serviceClient.GetDirectoryClient(directoryName);
                var fileStorage = directory.GetFileClient(fileName);
                await using(var data = file.OpenReadStream())
                {
                    await fileStorage.CreateAsync(data.Length);
                    await fileStorage.UploadAsync(data);
                }
            }
            public static async Task DeleteFileFolder(string fileName)
            {
                shareServiceClient = new ShareServiceClient(connectionstring);
                var serviceClient = shareServiceClient.GetShareClient(fileName);
                await serviceClient.DeleteIfExistsAsync();
            }
        }
        
    
}