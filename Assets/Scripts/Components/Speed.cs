using Unity.Entities;

namespace ZombieDOTS
{
    /// <summary>
    /// Represents speed values as meters per second.
    /// </summary>
    public struct Speed : IComponentData
    {
        public const float max = .1f;
        public float value;

        public float Percent
        {
            get => value / max;
            set => this.value = max * value;
        }
    }
}