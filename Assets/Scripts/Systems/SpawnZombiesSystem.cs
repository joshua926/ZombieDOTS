using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ZombieDOTS
{
    public partial class SpawnZombiesSystem : SystemBase
    {
        
        protected override void OnCreate()
        {
            EntityArchetype zombieArch = EntityManager.CreateArchetype(Zombie.ComponentTypes);
            Random rand = new Random(1);
            EntityManager.CreateEntity(zombieArch, 100);
            foreach (ZombieAspect z in SystemAPI.Query<ZombieAspect>())
            {
                z.Init((uint)UnityEngine.Random.Range(1, int.MaxValue));
                z.transformAspect.LocalPosition = rand.NextFloat3(new float3(-25, 0, -25), new float3(25, 0, 25));
            }
        }

        protected override void OnUpdate()
        {
            
        }
    }
}