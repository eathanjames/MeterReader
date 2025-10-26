using API.Endpoints;

namespace API.Config;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        AccountEndpoints.Map(app);
        MeterReaderEndpoints.Map(app);
    }
}