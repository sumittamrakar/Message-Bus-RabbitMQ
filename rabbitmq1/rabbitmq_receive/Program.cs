using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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
               channel.QueueBind(queue: "myq",
                                 exchange: "logs",
                                 routingKey: "");

               var consumer = new EventingBasicConsumer(channel);
               consumer.Received += Consumer_Received;
               channel.BasicConsume(queue: "myq",
                                    autoAck: true,
                                    consumer: consumer);
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
