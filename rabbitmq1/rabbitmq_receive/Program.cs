using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

               var consumer = new EventingBasicConsumer(channel);
               consumer.Received += Consumer_Received;
               channel.BasicConsume(queue: "myq", autoAck: true, consumer: consumer);
               Console.ReadLine();
            }
         }

      }

      private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
      {
         var body = e.Body;
         string msg = System.Text.Encoding.UTF8.GetString(body);
         Console.WriteLine($"received: {msg}");
      }
   }
}
