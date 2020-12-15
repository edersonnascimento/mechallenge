namespace MEChallenge.Application.Interfaces
{
    public interface IParamQuery<TResult, TParam>
    {
        TResult Query(TParam param);
    }
    public interface IQuery<TResult>
    {
        TResult Query();
    }
}
