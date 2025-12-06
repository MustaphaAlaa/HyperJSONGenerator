using Microsoft.Extensions.ObjectPool;

namespace Metriflow.HyperJSONGenerator.CustomPolicies;

// public static class RecordPool<T> where T : class, IAnalyticRecord, new()
// {
//     public static readonly ObjectPool<T> Pool =
//         new DefaultObjectPool<T>(new DefaultPooledObjectPolicy<T>());
// }