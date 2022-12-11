using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ZombieDOTS
{
    /// <summary> Copies entity transform values to MonoBehavior transform values. </summary>
    [UpdateAfter(typeof(CreateDestroyZombieGOSystem))]
    public partial class TransformSyncSystem : SystemBase
    {

        protected override void OnUpdate()
        {
            foreach (var (tr, ta) in SystemAPI.Query<TransformRef, LocalTransform>())
            {
                tr.value.localPosition = ta.Position;
                tr.value.localRotation = ta.Rotation;
                tr.value.localScale = new float3(ta.Scale);
            }
        }
    }
}