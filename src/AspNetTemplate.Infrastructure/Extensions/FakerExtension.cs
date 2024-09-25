using Bogus.DataSets;

namespace AspNetTemplate.Infrastructure.Extensions;

public static class FakerExtension
{
    public static string StrongPassword(this Internet internet)
    {
        return internet.Password(prefix: "A0_");
    }
}
