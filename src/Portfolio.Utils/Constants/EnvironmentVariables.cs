namespace Portfolio.Utils.Constants;

public static class EnvironmentVariables
{
    // Database
    public static string DATABASE_HOST => Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "";
    public static string DATABASE_NAME => Environment.GetEnvironmentVariable("DATABASE_NAME") ?? "";
    public static string DATABASE_PASSWORD => Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "";
    public static string DATABASE_PORT => Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "";
    public static string DATABASE_USER => Environment.GetEnvironmentVariable("DATABASE_USER") ?? "";

    // Json Web Token
    public static string JWT_AUDIENCE => Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "";
    public static string JWT_ISSUER => Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "";
    public static string JWT_SECRET => Environment.GetEnvironmentVariable("JWT_SECRET") ?? "";
}
