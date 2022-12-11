#pragma warning disable 0414
using Unity.Entities;
using Unity.Transforms;

namespace ZombieDOTS
{
    public readonly partial struct ZombieAspect : IAspect
    {
        public readonly Entity entity;
        public readonly TransformAspect transformAspect;
        readonly RefRO<Zombie> zombie;
        readonly RefRW<Speed> speed;
        readonly EnabledRefRW<PerformAttack> performAttack;

        public float Speed
        {
            get => speed.ValueRO.value;
            set => speed.ValueRW.value = value;
        }

        public bool PerformAttack
        {
            get => performAttack.ValueRO;
            set => performAttack.ValueRW = value;
        }
    }
}
#pragma warning restore 0414