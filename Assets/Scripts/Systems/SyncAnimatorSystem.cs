using UnityEngine;
using Unity.Entities;

namespace ZombieDOTS
{
    public partial class SyncAnimatorSystem : SystemBase
    {
        int speedID;
        int performAttackID;

        protected override void OnCreate()
        {
            speedID = Animator.StringToHash("Speed");
            performAttackID = Animator.StringToHash("PerformAttack");
        }

        protected override void OnUpdate()
        {
            foreach (var (speed, animator) in SystemAPI.Query<Speed, AnimatorRef>())
            {
                animator.value.SetFloat(speedID, speed.Percent);
            }
            var ecbBeginPres =
                SystemAPI.GetSingleton<BeginPresentationEntityCommandBufferSystem.Singleton>()
                    .CreateCommandBuffer(EntityManager.WorldUnmanaged);
            foreach (var (p, animator, entity) in SystemAPI.Query<PerformAttack, AnimatorRef>().WithEntityAccess())
            {
                animator.value.SetTrigger(performAttackID);
                ecbBeginPres.SetComponentEnabled<PerformAttack>(entity, false);
            }
        }
    }
}