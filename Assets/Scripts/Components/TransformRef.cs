using UnityEngine;
using Unity.Entities;

namespace ZombieDOTS
{
    public class TransformRef : ICleanupComponentData
    {
        public Transform value;
    }
}