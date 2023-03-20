using System;

namespace B3.Test.Domain.Entities
{
    public class Tarefa
    {
        public int TarefaId { get; set; }
        public int TarefaStatusId { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }
}
