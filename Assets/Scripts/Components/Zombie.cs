using Unity.Entities;
using Unity.Transforms;

namespace ZombieDOTS
{
    public struct Zombie : ICleanupComponentData
    {
        public static readonly ComponentType[] ComponentTypes = new[]
        {
            new ComponentType(typeof(Zombie)),
            new ComponentType(typeof(LocalTransform)),
            new ComponentType(typeof(ParentTransform)),
            new ComponentType(typeof(WorldTransform)),
            new ComponentType(typeof(Speed)),
            new ComponentType(typeof(PerformAttack))
        };
    }
}