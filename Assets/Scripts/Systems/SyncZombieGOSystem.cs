using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace ZombieDOTS
{
    public partial class SyncZombieGOSystem : SystemBase
    {
        GameObject zombiePrefab;

        protected override void OnCreate()
        {
            zombiePrefab = SettingsSO.Instance.Zombie;
        }

        protected override void OnUpdate()
        {
            var ecbEndSim =
                SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                    .CreateCommandBuffer(EntityManager.WorldUnmanaged);
            
            // Init
            // Find newly created zombies without TransformRef components and instantiate zombie prefabs for them.
            foreach (var (z, entity) in 
                     SystemAPI.Query<ZombieAspect>().
                         WithNone<TransformRef>().
                         WithEntityAccess().
                         WithOptions(EntityQueryOptions.IgnoreComponentEnabledState))
            {
                GameObject go = Object.Instantiate(zombiePrefab);
                go.transform.localPosition = z.transformAspect.LocalPosition;
                go.transform.localRotation = z.transformAspect.LocalRotation;
                go.transform.localScale = new float3(z.transformAspect.LocalScale);
                ecbEndSim.AddComponent(entity, new TransformRef { value = go.transform });
                ecbEndSim.AddComponent(entity, new AnimatorRef { value = go.GetComponent<Animator>() });
            }
            
            // Clean up
            // Find recently destroyed zombie entities. They will still have the TransformRef and Zombie components
            // but no AnimatorRef component. Destroy there GameObjects and remove the last components.
            foreach (var (transform, z, entity) in SystemAPI.Query<TransformRef, Zombie>().WithNone<AnimatorRef>()
                         .WithEntityAccess())
            {
                Object.Destroy(transform.value.gameObject);
                ecbEndSim.RemoveComponent<TransformRef>(entity);
                ecbEndSim.RemoveComponent<Zombie>(entity);
            }
        }
    }
}