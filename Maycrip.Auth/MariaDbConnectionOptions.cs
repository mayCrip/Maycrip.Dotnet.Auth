namespace Maycrip.Auth;

public class MariaDBConnectionOptions
{
    public string Server { get; set; }

    public string Port { get; set; } = "3306";

    public string Database { get; set; }

    public string User { get; set; }

    public string Password { get; set; }

    public string BuildConnectionString() =>
        $"Server={Server};Port={Port};Database={Database};Uid={User};Pwd={Password};SslMode=PREFERRED";
}