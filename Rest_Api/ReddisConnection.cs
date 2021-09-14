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
            showOper(obj.getOper());
            obj.setOper("insert");
            var dt = cache.KeyExists(JsonConvert.SerializeObject(obj));
            if(dt)
                 Console.WriteLine($"Data exists: {JsonConvert.SerializeObject(obj)}.");  
            else
                Console.WriteLine("Data does not exist in cache.");
           
        }  

        public void InsertData(Object obj, string message)  
        {  
            showOper(obj.getOper());
            obj.setOper("insert");

            int hash = message.GetHashCode();

            if(cache.KeyExists(message)) { Console.WriteLine("Data can not be inserted: It currently exists!"); return; }
    
            cache.StringAppend(message, hash);
            Console.WriteLine("The data has been inserted into cache succesfully!");
        }  

        public void DeleteData(Object obj, string message)  
        {   

            obj.setOper("insert");
            string  dt = JsonConvert.SerializeObject(obj);
            bool b = cache.KeyExists(dt);
            cache.KeyDelete(dt);
            
            if(b)
                Console.WriteLine("Data succesfuly deleted from cache.");
            else
                Console.WriteLine("Data can not be deleted: It does not exist!");
        }

        public void UpdateData(Object obj, string message)  
        {  
        }

        void showOper(string oper)
        {

        }            
    }
}