using B3.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace B3.Test.Data
{
    public class TarefaStatusMap : EntityMap
    {
        public TarefaStatusMap(ModelBuilder builder)
        {
            builder.Entity<TarefaStatus>(b => {
                b.ToTable("tarefa_status");
                b.HasKey(e => e.TarefaStatusId);
                b.Property(e => e.Descricao);
            });
        } 
    }
}
