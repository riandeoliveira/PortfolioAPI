namespace AspNetTemplate.Domain.Constants;

public static class EnvironmentVariables
{
    public static string CLIENT_URL => GetVariable("CLIENT_URL");

    public static string DATABASE_HOST => GetVariable("DATABASE_HOST");
    public static string DATABASE_NAME => GetVariable("DATABASE_NAME");
    public static string DATABASE_PASSWORD => GetVariable("DATABASE_PASSWORD");
    public static string DATABASE_PORT => GetVariable("DATABASE_PORT");
    public static string DATABASE_USER => GetVariable("DATABASE_USER");

    public static string MAIL_HOST => GetVariable("MAIL_HOST");
    public static string MAIL_PASSWORD => GetVariable("MAIL_PASSWORD");
    public static string MAIL_PORT => GetVariable("MAIL_PORT");
    public static string MAIL_SENDER => GetVariable("MAIL_SENDER");
    public static string MAIL_USERNAME => GetVariable("MAIL_USERNAME");

    public static string JWT_AUDIENCE => GetVariable("JWT_AUDIENCE");
    public static string JWT_ISSUER => GetVariable("JWT_ISSUER");
    public static string JWT_SECRET => GetVariable("JWT_SECRET");

    private static string GetVariable(string name) => Environment.GetEnvironmentVariable(name) ?? "";
}
