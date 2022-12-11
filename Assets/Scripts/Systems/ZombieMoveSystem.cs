using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;

namespace ZombieDOTS
{
    [BurstCompile]
    public partial struct ZombieMoveSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            Entity targetEntity = SystemAPI.GetSingletonEntity<ZombieTarget>();
            LocalTransform targetTransform = SystemAPI.GetComponent<LocalTransform>(targetEntity);
            float3 targetPosition = targetTransform.Position;
            foreach (ZombieAspect z in SystemAPI.Query<ZombieAspect>().WithOptions(EntityQueryOptions.IgnoreComponentEnabledState))
            {
                float distance = math.distance(targetPosition, z.transformAspect.LocalPosition);
                float3 forward = (targetPosition - z.transformAspect.LocalPosition) / distance;
                quaternion rotation = quaternion.LookRotation(forward, math.up());
                z.transformAspect.LocalRotation = rotation;
                if (distance > 3)
                {
                    z.transformAspect.LocalPosition += z.Speed * forward;
                }
            }
        }
    }
}