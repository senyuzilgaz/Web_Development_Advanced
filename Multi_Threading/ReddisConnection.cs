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

        public void InsertData(Object obj, string message, int num)  
        {  
            showOper(obj.getOper());
            obj.setOper("insert");

            int hash = message.GetHashCode();

            if(cache.KeyExists(message)) { Console.WriteLine("Thread: {0}, Data can not be inserted: It currently exists!", num); return; }
    
            cache.StringAppend(message, hash);
            Console.WriteLine("Thread: {0}, The data has been inserted into cache succesfully!", num);
        }  

        public void DeleteData(Object obj, string message, int num)  
        {   

            obj.setOper("insert");
            string  dt = JsonConvert.SerializeObject(obj);
            bool b = cache.KeyExists(dt);
            cache.KeyDelete(dt);
            
            if(b)
                Console.WriteLine("Thread: {0}, Data succesfuly deleted from cache.", num);
            else
                Console.WriteLine("Thread: {0}, Data can not be deleted: It does not exist!", num);
        }

        public void UpdateData(Object obj, string message, int num)  
        {  
        }

        void showOper(string oper)
        {

        }            
    }
}