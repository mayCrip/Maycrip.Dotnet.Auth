using System.Data;
using Dapper;

namespace Maycrip.Auth.Queries;

internal class GetUserByNameQuery : IDbQuery<AppUser>
{
    private const string sql = @"
SELECT id, login as username, password, role FROM users WHERE login = @Username;
";

    public string Username { get; set; }

    public async Task<AppUser> ExecuteAsync(IDbConnection connection)
    {
        if (connection == null)
        {
            throw new ArgumentNullException(nameof(connection));
        }

        try
        {
            var appUser = await connection.QuerySingleOrDefaultAsync<AppUser>(sql, this);

            return appUser;
        }
        catch
        {
            return null;
        }
    }
        
}