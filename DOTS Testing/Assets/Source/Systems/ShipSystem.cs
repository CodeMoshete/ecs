using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class ShipSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Rotation rotation) => 
        {
            quaternion currentRotation = math.mul(rotation.Value, quaternion.Euler(60/Mathf.PI, 0f, 0f));
            rotation.Value = currentRotation;
        });
    }
}
