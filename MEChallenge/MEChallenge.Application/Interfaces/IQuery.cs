namespace MEChallenge.Application.Interfaces
{
    public interface IQuery<TResult, TParam>
    {
        TResult Query(TParam param);
    }
}
