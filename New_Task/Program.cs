using System;
using System.IO;
using RabbitMQ.Client;
using System.Text;


namespace New_Task
{
    class Program
    {
        static void Main(string[] args)
        {
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


                using (var sr = new StreamReader("Data.txt"))
                {
                    
                    string text = sr.ReadToEnd();
                    string[] items = text.Split(';');
                    foreach (var item in items)
                    {
                        var message = item;
                        var body = Encoding.UTF8.GetBytes(message);

                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;

                        channel.BasicPublish(exchange: "",
                                            routingKey: "task_queue",
                                            basicProperties: properties,
                                            body: body);
                        Console.WriteLine("Sent");
                    }
                }

            }
            
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}