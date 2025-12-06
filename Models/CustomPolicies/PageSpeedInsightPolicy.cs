using Microsoft.Extensions.ObjectPool;

namespace Metriflow.HyperJSONGenerator.CustomPolicies;

// public class PageSpeedInsightPolicy : PooledObjectPolicy<PageSpeedInsight>
// {
//     public override PageSpeedInsight Create()
//     {
//         return new();
//     }

//     public override bool Return(PageSpeedInsight obj)
//     {
//              return  obj.TryReset() ? true : false;

//     }
// }