using Unity.Entities;
using Unity.Mathematics;

namespace ZombieDOTS
{
    public struct RandomComponent : IComponentData
    {
        public Random value;
    }
}