using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ShipJobSystem : JobComponentSystem
{
    private struct ShipRotation : IJobForEach<Rotation>
    {
        public void Execute(ref Rotation rotation)
        {
            quaternion currentRotation = math.mul(rotation.Value, quaternion.Euler(0f, 60 / Mathf.PI, 0f));
            rotation.Value = currentRotation;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        ShipRotation job = new ShipRotation();
        return job.Schedule(this, inputDeps);
    }
}
