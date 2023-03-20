using B3.Test.Domain.Entities;
using B3.Test.Domain.Interfaces.Repositories;
using B3.Test.Domain.Interfaces.Services;

namespace B3.Test.Domain.Services
{
    public class ServiceTarefaStatus : ServiceBase<TarefaStatus>, IServiceTarefaStatus
    {
        protected IRepositoryTarefaStatus _tarefaStatusRepository;

        public ServiceTarefaStatus(IRepositoryTarefaStatus tarefaStatusRepository) :base(tarefaStatusRepository)
        {
            _tarefaStatusRepository = tarefaStatusRepository;
        }
    }
}
