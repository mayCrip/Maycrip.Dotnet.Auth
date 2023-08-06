using System.Data;
using Dapper;

namespace Maycrip.Auth.Queries;

internal class UpdateUserQuery : IDbQuery
{
    private const string Query = @"
UPDATE users
SET
    login = @Username,
    password = @Password
    [role] = @Role
WHERE
    id = @Id;
";
    public AppUser User { get; init; }

    public Task ExecuteAsync(IDbConnection connection)
        => connection.ExecuteAsync(Query, User);
}