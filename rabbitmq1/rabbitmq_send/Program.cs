using System;
using RabbitMQ.Client;

namespace rabbitmq_receive
{
   class Program
   {
      static void Main(string[] args)
      {
         ConnectionFactory factory = new ConnectionFactory();
         factory.HostName = "127.0.0.1";
         // Use predefined username and password or one obtained when rabbitMQ was configured
         factory.UserName = "user";
         factory.Password = "0hGxXRatU3";
         //factory.VirtualHost = "/";

         using (IConnection connection = factory.CreateConnection())
         {
            using (IModel channel = connection.CreateModel())
            {
               channel.QueueDeclare(queue: "myq", durable: false, exclusive: false, autoDelete: false, arguments: null);

               string msg = "Hello, World! It's Sumit.";
               var body = System.Text.Encoding.UTF8.GetBytes(msg);

               channel.BasicPublish(exchange: "", routingKey: "myq", basicProperties: null, body: body);

               Console.WriteLine($"sent: {msg}");
            }
         }

      }
   }
}
