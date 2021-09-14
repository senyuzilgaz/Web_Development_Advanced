using System;
using System.Text.Json;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestApi
{
    public class Redisconnection
    {

        StackExchange.Redis.IDatabase cache = RedisConnectorHelper.Connection.GetDatabase();

        public void ReadData(Object obj, string message)  
        {  
            obj.setOper("insert");
            var dt = cache.KeyExists(JsonConvert.SerializeObject(obj));
            if(dt)
                 Console.WriteLine($"Data exists: {JsonConvert.SerializeObject(obj)}.");  
            else
                Console.WriteLine("Data does not exist in cache.");
        }  

        public void InsertData(Object obj, string message)  
        {  
            int hash = message.GetHashCode();
    
            cache.StringAppend(message, hash);
        }  

        public void UpdateData(Object obj, string message)  
        {  
        }
         
    }
}