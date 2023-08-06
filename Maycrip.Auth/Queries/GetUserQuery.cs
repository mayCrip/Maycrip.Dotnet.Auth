using System.Data;
using Dapper;

namespace Maycrip.Auth.Queries;

internal class GetUserQuery : IDbQuery<AppUser>
{
    private const string sql = @"
SELECT id, login as username, password, role FROM user WHERE id = @UserId;
";

    public long UserId { get; set; }

    public Task<AppUser> ExecuteAsync(IDbConnection connection)
        => connection.QuerySingleOrDefaultAsync<AppUser>(sql, this);
}