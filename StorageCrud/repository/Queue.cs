using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
namespace StorageCrud.repository
{
    public class Queue
    {
        static string connectionString = "DefaultEndpointsProtocol=https;AccountName=accountstorage301;AccountKey=v9YB30ihqtRozS/RdXvmfkX7rrsZOy/lq+vLCMyFPwjeD7sANpSfp5G3CzsDXn9mJT/nLNR9MCJU+AStJsDmrg==;EndpointSuffix=core.windows.net";
        public static async Task<bool> CreateQueue(string queueName)
        {
            if(string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentNullException("enter queue name");
            }
            try{
                QueueClient container =new QueueClient(connectionString,queueName);
                await container.CreateIfNotExistsAsync();
                if(container.Exists())
                {
                    Console.WriteLine("queue created :"+container.Name);
                    return true;
                }
                else
                {
                    Console.WriteLine("check and try again");
                    return false;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public static async Task InsertMessage(string queueName,string msg)
        {
            if(string.IsNullOrEmpty(msg))
            {
                throw new ArgumentNullException("enter message");
            }
            QueueClient container = new QueueClient(connectionString,queueName);
            await container.CreateIfNotExistsAsync();
            if(container.Exists())
            {
                var data = container.SendMessage(msg);
                Console.WriteLine("message sent");
            }
            else
            {
                Console.WriteLine("message not sent");
            }
        }
        public static async Task<PeekedMessage[]> peekMessage(string queueName)
        {
            QueueClient container = new QueueClient(connectionString,queueName);
            PeekedMessage[] msg = null;
            if(container.Exists())
            {
                msg = container.PeekMessages(2);
            }
            return msg;
        }
        public static async Task UpdateMessage(string queueName,string data)
        {
            QueueClient container = new QueueClient(connectionString,queueName);
            if(container.Exists())
            {
                QueueMessage[] msg=container.ReceiveMessages();
                container.UpdateMessage(msg[0].MessageId,msg[0].PopReceipt,data,TimeSpan.FromSeconds(100));
            }
        }
        public static async Task DequeueMessage(string queueName)
        {
            QueueClient container = new QueueClient(connectionString,queueName);
            if(container.Exists())
            {
                QueueMessage[] msg = container.ReceiveMessages();
                System.Console.WriteLine("Dequeue message"+msg[0].Body);
                container.DeleteMessage(msg[0].MessageId,msg[0].PopReceipt);
            }
        }
        public static async Task DeleteQueue(string queueName)
        {
            QueueClient container = new QueueClient(connectionString,queueName);
            if(container.Exists())
            {
                await container.DeleteAsync();
            }
        }
        
    }
}