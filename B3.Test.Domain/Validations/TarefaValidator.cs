using B3.Test.Domain.ViewModel;
using FluentValidation;

namespace B3.Test.Domain.Validations
{
    public class TarefaValidator : AbstractValidator<TarefaCadastroViewModel>
    {
        public TarefaValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("A descrição da tarefa precisa ser informada.")
                .MaximumLength(50)
                .WithMessage("A descrição teve ter no máximo 50 caracteres");
        }
    }
}
