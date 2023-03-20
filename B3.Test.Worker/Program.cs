using System;

namespace B3.Test.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("######### WORKER B3 TAREFAS ############");
            Console.WriteLine("######### FLOW CADASTRO DE TAREFAS     ############");
            RabbitMqWorker.Init();
            Console.ReadLine();
        }
    }
}
