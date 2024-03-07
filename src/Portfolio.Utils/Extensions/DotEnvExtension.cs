namespace Portfolio.Users.Environments;

public static class DotEnvExtension
{
    public static void Load()
    {
        string appRoot = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(appRoot, "..", "..", ".env");

        Save(filePath);
    }

    private static void Save(string filePath)
    {
        if (!File.Exists(filePath)) return;

        foreach (string line in File.ReadAllLines(filePath))
        {
            string[] parts = line.Split("=", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2) continue;

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}
