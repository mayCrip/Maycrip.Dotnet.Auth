using System.Data;

namespace Maycrip.Auth;

internal interface IDbQuery
{
    Task ExecuteAsync(IDbConnection connection);
}

internal interface IDbQuery<TResult>
{
    Task<TResult> ExecuteAsync(IDbConnection connection);
}