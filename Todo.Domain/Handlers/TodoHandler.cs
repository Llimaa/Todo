using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.HandLers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.HandLers
{
    public class TodoHandler :
        Notifiable,
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUndoneCommand>
    {

        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateTodoCommand command)
        {
            // Fail Fast Validation

            command.Validate();
            if (!command.Valid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            //Gerar todoItem
            var todo = new TodoItem(command.Title, command.User, command.Date);

            //salvar no banco
            _repository.Create(todo);

            //retornar resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            // Recuperar o TodoItem (Rehidratação)
            var todo = _repository.GetById(command.Id, command.User);

            //ALterar titulo
            todo.UpdateTitle(command.Title);

            //Salvar no banco
            _repository.update(todo);

            //Retornar resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(MarkTodoAsDoneCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            // Recuperar o TodoItem (Rehidratação)
            var todo = _repository.GetById(command.Id, command.User);

            //ALterar estado
            todo.MarkAsDone();

            //Salvar no banco
            _repository.update(todo);

            //Retornar resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(MarkTodoAsUndoneCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);

            // Recuperar o TodoItem (Rehidratação)
            var todo = _repository.GetById(command.Id, command.User);

            //ALterar estado
            todo.MarkAsUnone();

            //Salvar no banco
            _repository.update(todo);

            //Retornar resultado
            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}