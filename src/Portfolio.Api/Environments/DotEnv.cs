namespace Portfolio.Users.Environments;

public static class DotEnv
{
    public static void Load()
    {
        var appRoot = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(appRoot, "..", "..", ".env");

        Save(filePath);
    }

    private static void Save(string filePath)
    {
        if (!File.Exists(filePath)) return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split("=", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2) continue;

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}
