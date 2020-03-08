using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;

public class Engine : MonoBehaviour
{
    public Mesh meshObject;
    public Material meshMaterial;
    public int numHorizontal;
    public int numVertical;
    public int numDepth;
    public float PaddingX;
    public float PaddingY;
    public float PaddingZ;
    private EntityManager entityManager;
    private EntityArchetype shipArchetype;
    private List<Entity> shipEntities;

    private void Start()
    {
        shipEntities = new List<Entity>();

        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        shipArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(Scale),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
        );

        SpawnShips();
    }

    private void ClearShips()
    {
        for (int i = 0; i < shipEntities.Count; ++i)
        {
            entityManager.DestroyEntity(shipEntities[i]);
        }
    }

    private void SpawnShips()
    {
        for (int i = 0; i < numHorizontal; ++i)
        {
            for (int j = 0; j < numVertical; ++j)
            {
                for (int k = 0; k < numDepth; ++k)
                {
                    float xPos = PaddingX * i;
                    float yPos = PaddingY * j;
                    float zPos = PaddingZ * k;
                    SpawnShip(xPos, yPos, zPos);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearShips();
            SpawnShips();
        }
    }

    private void SpawnShip(float xPos, float yPos, float zPos)
    {
        Entity newEntity = entityManager.CreateEntity(shipArchetype);

        Translation translation = new Translation();
        translation.Value = new float3(xPos, yPos, zPos);
        entityManager.AddComponentData(newEntity, translation);

        Rotation rotation = new Rotation();
        rotation.Value = quaternion.Euler(new float3(-Mathf.PI / 2, 0f, 0f));
        entityManager.AddComponentData(newEntity, rotation);

        Scale scale = new Scale();
        scale.Value = 100f;
        entityManager.AddComponentData(newEntity, scale);

        RenderMesh entityMesh = new RenderMesh();
        entityMesh.mesh = meshObject;
        entityMesh.material = meshMaterial;
        entityManager.AddSharedComponentData(newEntity, entityMesh);

        shipEntities.Add(newEntity);
    }
}
