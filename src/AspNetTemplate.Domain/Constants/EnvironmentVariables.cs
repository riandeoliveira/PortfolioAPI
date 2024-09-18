namespace AspNetTemplate.Domain.Constants;

public static class EnvironmentVariables
{
    // Client
    public static string CLIENT_URL => Environment.GetEnvironmentVariable("CLIENT_URL") ?? "";

    // Database
    public static string DATABASE_HOST => Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "";
    public static string DATABASE_NAME => Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "";
    public static string DATABASE_PASSWORD => Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "";
    public static string DATABASE_PORT => Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "";
    public static string DATABASE_USER => Environment.GetEnvironmentVariable("DATABASE_USER") ?? "";

    // Mail
    public static string MAIL_HOST => Environment.GetEnvironmentVariable("MAIL_HOST") ?? "";
    public static string MAIL_PASSWORD => Environment.GetEnvironmentVariable("MAIL_PASSWORD") ?? "";
    public static string MAIL_PORT => Environment.GetEnvironmentVariable("MAIL_PORT") ?? "";
    public static string MAIL_SENDER => Environment.GetEnvironmentVariable("MAIL_SENDER") ?? "";
    public static string MAIL_USERNAME => Environment.GetEnvironmentVariable("MAIL_USERNAME") ?? "";

    // JWT
    public static string JWT_AUDIENCE => Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "";
    public static string JWT_ISSUER => Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "";
    public static string JWT_SECRET => Environment.GetEnvironmentVariable("JWT_SECRET") ?? "";
}
