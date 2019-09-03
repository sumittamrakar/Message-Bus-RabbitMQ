using System;
using RabbitMQ.Client;

namespace rabbitmq_receive
{
   class Program
   {
      static void Main(string[] args)
      {
         ConnectionFactory factory = new ConnectionFactory()
         {
            HostName = "127.0.0.1",
            UserName = "user",
            Password = "0hGxXRatU3"
         };

         using (IConnection connection = factory.CreateConnection())
         {
            using (IModel channel = connection.CreateModel())
            {
               channel.ExchangeDeclare(exchange: "logs", 
                                       type: "fanout");

               channel.QueueDeclare(queue: "myq", 
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

               string msg = "Hello, World! It's Sumit.";
               var body = System.Text.Encoding.UTF8.GetBytes(msg);

               channel.BasicPublish(exchange: "logs",
                                    routingKey: "myq",
                                    basicProperties: null,
                                    body: body);

               Console.WriteLine($"sent: {msg}");
            }
         }

      }
   }
}
