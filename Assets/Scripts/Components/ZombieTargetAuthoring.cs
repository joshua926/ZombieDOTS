using UnityEngine;
using Unity.Entities;

namespace ZombieDOTS
{
    public struct ZombieTarget : IComponentData
    {
        
    }

    public class ZombieTargetAuthoring : MonoBehaviour
    {
        
    }

    public class ZombieTargetBaker : Baker<ZombieTargetAuthoring>
    {
        public override void Bake(ZombieTargetAuthoring authoring)
        {
            AddComponent(new ZombieTarget());
        }
    }
}