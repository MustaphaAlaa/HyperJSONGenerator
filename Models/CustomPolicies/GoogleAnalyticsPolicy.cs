using Microsoft.Extensions.ObjectPool;

namespace Metriflow.HyperJSONGenerator.CustomPolicies;

public class GoogleAnalyticsPolicy : PooledObjectPolicy<GoogleAnalytics>
{
    public override GoogleAnalytics Create()
    {
        return new();
    }

    public override bool Return(GoogleAnalytics obj)
    {
        obj.TryReset();
        return true;
    }
}