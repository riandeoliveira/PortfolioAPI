using Bogus.DataSets;

namespace AspNetTemplate.Infra.Common.Extensions;

public static class FakerExtension
{
    public static string StrongPassword(this Internet internet)
    {
        return internet.Password(prefix: "A0_");
    }
}
