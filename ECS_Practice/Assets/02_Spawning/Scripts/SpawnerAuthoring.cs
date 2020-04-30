using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

namespace DOTS_Practice.Spawning{
    [DisallowMultipleComponent]
    [RequiresEntityConversion]
    public class SpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {

        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _maxDistanceFromSpawner;

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs){
            referencedPrefabs.Add(_prefab);
        }
        
        

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new Spawner{
                prefab = conversionSystem.GetPrimaryEntity(_prefab),
                maxDistanceFromSpawner = _maxDistanceFromSpawner,
                secondsBetweenSpawns = 1 / _spawnRate,
                secondsToNextSpawn = 0f
            });
        }

    }
}
