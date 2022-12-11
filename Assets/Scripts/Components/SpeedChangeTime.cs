using Unity.Entities;

namespace ZombieDOTS
{
    public struct SpeedChangeTime : IComponentData
    {
        public double lastChangeTime;
        public double duration;
    }
}