namespace AspNetTemplate.Domain.Constants;

public static class DatabaseAccess
{
    public static string CONNECTION_STRING =>
    $@"
        Server={EnvironmentVariables.DATABASE_HOST};
        Port={EnvironmentVariables.DATABASE_PORT};
        Database={EnvironmentVariables.DATABASE_NAME};
        User Id={EnvironmentVariables.DATABASE_USER};
        Password={EnvironmentVariables.DATABASE_PASSWORD}
    ";
}
