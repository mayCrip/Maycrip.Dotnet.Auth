namespace Maycrip.Auth;

/// <summary>
/// Application User
/// </summary>
public class AppUser
{
    /// <summary>
    /// Unique Identifier
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// User Password
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// User Role
    /// </summary>
    public string Role { get; set; }    
}