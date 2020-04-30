using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace DOTS_Practice.Spawning {
    [Serializable]
    public struct Spawner : IComponentData
    {
        public Entity prefab;
        public float maxDistanceFromSpawner;
        public float secondsBetweenSpawns;
        public float secondsToNextSpawn;        
    }
}
