using System;
using System.IO;
using System.Threading;

namespace RestApi
{
    class Program  
    {  
        static void Main(string[] args)  
        {  
            Redisconnection redis_connection = new Redisconnection();


            Thread Worker_1 = new Thread(
                () => RestApi.Worker.rabbitcon( redis_connection, 1 ) );
            Worker_1.Start();


            Thread Worker_2 = new Thread(
                () => RestApi.Worker.rabbitcon( redis_connection, 2 ) );
            Worker_2.Start();


            Thread Worker_3 = new Thread(
                () => RestApi.Worker.rabbitcon( redis_connection, 3 ) );
            Worker_3.Start();


            Thread Worker_4 = new Thread(
                () => RestApi.Worker.rabbitcon( redis_connection, 4 ) );
            Worker_4.Start();
        }  

    }  
}