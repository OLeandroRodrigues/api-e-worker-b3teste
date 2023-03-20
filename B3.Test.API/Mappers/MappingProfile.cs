using AutoMapper;
using B3.Test.Domain.Entities;
using B3.Test.Domain.ViewModel;

namespace B3.Test.API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Tarefa
            CreateMap<Tarefa, TarefaCadastroViewModel>();
            CreateMap<TarefaCadastroViewModel, Tarefa>();

            CreateMap<Tarefa, TarefaUpdateViewModel>();
            CreateMap<TarefaUpdateViewModel, Tarefa>();
        }
    }
}
