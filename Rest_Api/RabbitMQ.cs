using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Newtonsoft.Json;

namespace RestApi
{
    class Worker
    {
        public static void rabbitcon( Redisconnection redis_connection )
        {
            Consume temp = new Consume();

            var cache = RedisConnectorHelper.Connection.GetDatabase(); 
            var factory = new ConnectionFactory() { HostName = "localhost",
                                                    UserName="ilgaz",
                                                    Password= "dededede"};
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Object obj = new Object();
                    Console.WriteLine(message);
                    obj = JsonConvert.DeserializeObject<Object>(message);
                    message = JsonConvert.SerializeObject(obj);
                    switch(obj.getOper() )
                    {
                        case "insert":  redis_connection.InsertData(obj, message);      break;
                        case "update":  redis_connection.UpdateData(obj, message);      break;
                        case "delete":  redis_connection.DeleteData(obj, message);      break;
                        case "read":    redis_connection.ReadData(obj, message);        break;
                        default: Console.WriteLine("Operation is neccesary, please provide data correctly!");       break;                
                    }
                    
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    temp.Insert(obj.getName(), obj.getComplete(), obj.getOper());
                    
                };
                channel.BasicConsume(queue: "task_queue",
                                    autoAck: false,
                                    consumer: consumer);
                

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }

}