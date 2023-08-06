using System.Data;
using Dapper;

namespace Maycrip.Auth.Queries;

internal class CreateUserQuery : IDbQuery<long>
{
    public const string sql = @"
INSERT INTO users (login, password, role)
VALUES (@Username, @Password, @Role)
RETURNING id;
";

    public AppUser User { get; set; }

    public async Task<long> ExecuteAsync(IDbConnection connection)
    {
        if (connection == null)
        {
            throw new ArgumentNullException(nameof(connection));
        }

        try
        {
            var userId = await connection.ExecuteScalarAsync<long>(sql, User);

            return userId;
        }
        catch
        {
            return -1;
        }
    }
}