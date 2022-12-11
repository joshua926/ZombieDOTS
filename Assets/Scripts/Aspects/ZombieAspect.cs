#pragma warning disable 0414
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ZombieDOTS
{
    public readonly partial struct ZombieAspect : IAspect
    {
        public readonly Entity entity;
        public readonly TransformAspect transformAspect;
        readonly RefRO<Zombie> zombie;
        readonly RefRW<Speed> speed;
        readonly RefRW<SpeedChangeTime> speedChange;
        readonly EnabledRefRW<PerformAttack> performAttack;
        readonly RefRW<RandomComponent> rand;

        public float Speed
        {
            get => speed.ValueRO.value;
            set => speed.ValueRW.value = value;
        }

        public float SpeedPercent
        {
            get => speed.ValueRO.Percent;
            set => speed.ValueRW.Percent = value;
        }

        public bool PerformAttack
        {
            get => performAttack.ValueRO;
            set => performAttack.ValueRW = value;
        }

        public SpeedChangeTime SpeedChange
        {
            get => speedChange.ValueRO;
            set => speedChange.ValueRW = value;
        }

        public Random Random
        {
            get => rand.ValueRO.value;
            set => rand.ValueRW.value = value;
        }

        public void Init(uint seed)
        {
            transformAspect.LocalPosition = 0;
            transformAspect.LocalScale = 1;
            transformAspect.LocalRotation = quaternion.identity;
            Speed = 0;
            PerformAttack = false;
            Random = new Random(seed);
        }
    }
}
#pragma warning restore 0414