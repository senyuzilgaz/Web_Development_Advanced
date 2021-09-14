using System;
using System.IO;

namespace RestApi
{
    class Program  
    {  
        static void Main(string[] args)  
        {  
            //Creates Redis connection
            Redisconnection redis_connection = new Redisconnection();

            //Starts the rabbitmq server and listens on task_queue channel
            //If input is aproppriate, inserts, updates, or deletes it from the cache; According to operator value
            RestApi.Worker.rabbitcon( redis_connection );
        }  

    }  
}