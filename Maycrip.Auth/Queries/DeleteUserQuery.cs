using System.Data;
using Dapper;

namespace Maycrip.Auth.Queries;

internal class DeleteUserQuery : IDbQuery
{
    private const string sql = @"
DELETE FROM users WHERE id = @{nameof(ApplicationUser.Id)}
";

    public AppUser User { get; set; }

    public Task ExecuteAsync(IDbConnection connection)
        => connection.ExecuteAsync(sql, User);
}