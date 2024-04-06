using Bogus.DataSets;

namespace Portfolio.Infrastructure.Extensions;

public static class FakerExtension
{
    /// <summary>
    /// Generates a random strong password.
    /// </summary>
    /// <returns>A random strong password.</returns>
    public static string StrongPassword(this Internet internet)
    {
        return internet.Password(prefix: "A0_");
    }
}
