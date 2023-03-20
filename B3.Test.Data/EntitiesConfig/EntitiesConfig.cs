using Microsoft.EntityFrameworkCore;

namespace B3.Test.Data
{
    public class EntitiesConfig
    {
        public EntitiesConfig(ModelBuilder builder)
        {
            
            //builder.Entity<Tarefa>(b => {
            //    b.HasKey(e => e.TarefaId);
            //    b.Property(e => e.Descricao);
            //    b.Property(e => e.Data);
            //    b.Property(e => e.TipoStatusId);
            //});
        }
    }
}
