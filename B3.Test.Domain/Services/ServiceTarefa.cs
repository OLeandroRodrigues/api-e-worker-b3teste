using B3.Test.Domain.Entities;
using B3.Test.Domain.Interfaces.Repositories;
using B3.Test.Domain.Interfaces.Services;

namespace B3.Test.Domain.Services
{
    public class ServiceTarefa : ServiceBase<Tarefa>, IServiceTarefa
    {
        protected IRepositoryTarefa _tarefaRepository;

        public ServiceTarefa(IRepositoryTarefa tarefaRepository):base(tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }
    }
}
