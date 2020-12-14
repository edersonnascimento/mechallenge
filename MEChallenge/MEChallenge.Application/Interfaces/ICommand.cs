namespace MEChallenge.Application.Interfaces
{
    public interface ICommand<T>
    {
        void Execute(T entity);
    }
}
