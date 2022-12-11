using Unity.Collections;
using UnityEngine;
using Unity.Entities;

namespace ZombieDOTS
{
    public partial class CreateDestroyZombieGOSystem : SystemBase
    {
        GameObject zombiePrefab;

        protected override void OnCreate()
        {
            zombiePrefab = PrefabStoreSO.Singleton.Zombie;
        }

        protected override void OnUpdate()
        {
            InitZombies();
            CleanUpZombies();
        }

        /// <summary>
        /// Find newly created zombies without TransformRef components and instantiate zombie prefabs for them.
        /// </summary>
        void InitZombies()
        {
            var ecbBeginSim =
                SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                    .CreateCommandBuffer(EntityManager.WorldUnmanaged);
            foreach (var (z, entity) in SystemAPI.Query<Zombie>().WithNone<TransformRef>().WithEntityAccess())
            {
                GameObject go = GameObject.Instantiate(zombiePrefab);
                ecbBeginSim.AddComponent(entity, new TransformRef { value = go.transform });
                ecbBeginSim.AddComponent(entity, new AnimatorRef { value = go.GetComponent<Animator>() });
            }
        }

        /// <summary>
        /// Find recently destroyed zombie entities. They will still have the TransformRef and Zombie components
        /// but no AnimatorRef component. Destroy there GameObjects and remove the last components.
        /// </summary>
        void CleanUpZombies()
        {
            var ecbEndSim =
                SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                    .CreateCommandBuffer(EntityManager.WorldUnmanaged);
            foreach (var (transform, z, entity) in SystemAPI.Query<TransformRef, Zombie>().WithNone<AnimatorRef>()
                         .WithEntityAccess())
            {
                GameObject.Destroy(transform.value.gameObject);
                ecbEndSim.RemoveComponent<TransformRef>(entity);
                ecbEndSim.RemoveComponent<Zombie>(entity);
            }
        }
    }
}