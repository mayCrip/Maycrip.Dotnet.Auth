namespace Maycrip.Auth;

internal interface IQueryRunner
{
    Task RunAsync(IDbQuery query);

    Task<TResult> RunAsync<TResult>(IDbQuery<TResult> query);    
}