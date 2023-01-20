using Azure.Data.Tables;
using StorageCrud.Model;
using Microsoft.AspNetCore.Mvc;
using StorageCrud.repository;
namespace StorageCrud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableStorageController
    {
        [HttpPost("AddTable")]
        public async Task<string> AddTable(string tableName)
        {
         await TableStorage.AddTable(tableName);
         return null;   
        }

        [HttpPut("UpdateTble")]
        public async Task<Details> Updatetable(Details employee,string tableName)
        {
            var data = await TableStorage.UpdateTable(employee,tableName);
            return data;
        }

        [HttpGet("GetTableData")]
        public async Task<Details> GetTableData(string tableName,string partitionKey,string rowKey)
        {
            var data = await TableStorage.GetTableData(tableName,partitionKey,rowKey);
            return data;
        }
        [HttpGet("GetTable")]
        public async Task<TableClient> GetTable(string tableName)
        {
            var data = await TableStorage.GetTable(tableName);
            return data;
        }
        [HttpDelete("DeleteTableData")]
        public async Task DeleteTableData(string tableName,string partitionKey,string rowKey)
        {
            await TableStorage.DeleteTableData(tableName,partitionKey,rowKey);
            return;
        }
        [HttpDelete("DeleteTable")]
        public async Task DeleteTable(string tableName)
        {
            await TableStorage.DeleteTable(tableName);
        }
        
    }
}