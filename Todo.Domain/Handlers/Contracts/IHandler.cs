using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.HandLers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}