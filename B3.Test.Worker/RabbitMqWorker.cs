using B3.Test.Domain.ViewModel;
using B3.Test.RabbitMq.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace B3.Test.Worker
{
    public class TemplateMessage
    {
        public string Message { get; set; }
        public string Name { get; set; }
    }
    public class RabbitMqWorker
    {

        public static void Init()
        {
            B3RabbitMq b3RabbitMq = new B3RabbitMq();
            b3RabbitMq.InitiateConnectionFactory(ConfigurationManager.AppSettings["rabbitmq-provider-connection"]);
            
            // Consumer
            bool autoAck = true;
            using var channel = b3RabbitMq.ConnnectionFactory.CreateConnection().CreateModel(); ;
            channel.QueueDeclare("cadastro-tarefa-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = b3RabbitMq.EventBasicConsumer(channel);
            try
            {
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(body)).Message;
                    var tarefa = new TarefaCadastroViewModel() { Descricao = message.Descricao, Data = message.Data, TarefaStatusId = message.TarefaStatusId };
                    var result = HttpClientPost(ConfigurationManager.AppSettings["tarefa-cadastro-api"], tarefa);

                    if (result == "falha")
                    {
                        // TODO :
                        // Criar uma Fila para DeadLetter 
                    }
                };
            }
            catch (Exception ex)
            {
                // poderia chamar o log aqui, implementei puro sem abstrações, mas não fiz a chamada, minha ideia seria salvar em bloco de texto mesmo. 
                autoAck = false;
            }

            channel.BasicConsume("cadastro-tarefa-queue", autoAck, consumer);

            Console.ReadLine();
        }

        private static string HttpClientPost(string url, TarefaCadastroViewModel tarefaViewModel = null)
        {
            string message = string.Empty;
            using var client = new HttpClient();

            client.BaseAddress = new Uri($"{url}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestMessage = string.Empty;
            requestMessage = JsonConvert.SerializeObject(tarefaViewModel);

            var content = new StringContent(requestMessage, Encoding.UTF8, "application/json");

            var response = client.PostAsync($"{url}", content).Result;
            if (response.IsSuccessStatusCode)
                message = response.Content.ReadAsStringAsync().Result;
            else
                message = "falha";

            return message;
        }
    }

}
