using Maycrip.Auth.Queries;
using Microsoft.AspNetCore.Identity;

namespace Maycrip.Auth;

internal class MariaDbUserStore : IUserPasswordStore<AppUser>, IRoleStore<AppUserRole>
{
    private readonly IQueryRunner _queryRunner;

    public MariaDbUserStore(IQueryRunner queryRunner)
    {
        _queryRunner = queryRunner ?? throw new ArgumentNullException(nameof(queryRunner));
    }

    public void Dispose() { }

    public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        => Task.FromResult(user.Id.ToString());

    public Task<string?> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        => Task.FromResult(user.Username);

    public Task SetUserNameAsync(AppUser user, string? userName, CancellationToken cancellationToken)
    {
        user.Username = userName;

        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        => Task.FromResult(user.Username);

    public Task SetNormalizedUserNameAsync(AppUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        user.Username = normalizedName;

        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        user.Id = await _queryRunner.RunAsync(new CreateUserQuery { User = user });

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _queryRunner.RunAsync(new UpdateUserQuery { User = user });

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _queryRunner.RunAsync(new DeleteUserQuery { User = user });

        return IdentityResult.Success;
    }

    public Task<IdentityResult> CreateAsync(AppUserRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(AppUserRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(AppUserRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetRoleIdAsync(AppUserRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetRoleNameAsync(AppUserRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetRoleNameAsync(AppUserRole role, string? roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetNormalizedRoleNameAsync(AppUserRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedRoleNameAsync(AppUserRole role, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<AppUserRole?> IRoleStore<AppUserRole>.FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<AppUserRole?> IRoleStore<AppUserRole>.FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<AppUser?> IUserStore<AppUser>.FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (long.TryParse(userId, out var userIdParsed))
        {
            return _queryRunner.RunAsync(new GetUserQuery { UserId = userIdParsed });
        }

        return Task.FromResult<AppUser>(null);
    }

    Task<AppUser?> IUserStore<AppUser>.FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(normalizedUserName))
        {
            throw new ArgumentNullException(nameof(normalizedUserName));
        }

        return _queryRunner.RunAsync(new GetUserByNameQuery { Username = normalizedUserName });
    }

    public Task SetPasswordHashAsync(AppUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        user.Password = passwordHash;

        return Task.CompletedTask;
    }

    public Task<string?> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        => Task.FromResult(user.Password);

    public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        => Task.FromResult(!string.IsNullOrWhiteSpace(user.Password));
}