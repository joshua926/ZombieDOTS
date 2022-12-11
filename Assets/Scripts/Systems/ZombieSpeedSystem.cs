using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;

namespace ZombieDOTS
{
    [BurstCompile]
    public partial struct ZombieSpeedSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            double time = SystemAPI.Time.ElapsedTime;
            foreach (var z in SystemAPI.Query<ZombieAspect>().WithOptions(EntityQueryOptions.IgnoreComponentEnabledState))
            {
                SpeedChangeTime speedChange = z.SpeedChange;
                if (time > speedChange.lastChangeTime + speedChange.duration)
                {
                    Random rand = z.Random;
                    float percent = rand.NextFloat();
                    z.SpeedPercent = percent * percent;
                    speedChange.lastChangeTime = time;
                    speedChange.duration = rand.NextFloat(5, 10);
                    z.Random = rand;
                    z.SpeedChange = speedChange;
                }
            }
        }
    }
}