using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

namespace DOTS_Practice.Spawning{
    public class SpawnerSystem : JobComponentSystem
    {
        
        private EndSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;

        protected override void OnCreate(){
            _endSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        struct SpawnerSystemJob : IJobForEachWithEntity<Spawner, LocalToWorld>
        {
            private EntityCommandBuffer.Concurrent entityCommandBuffer;
            private readonly float deltaTime;
            private Random random;

            public SpawnerSystemJob(EntityCommandBuffer.Concurrent entityCommandBuffer, float deltaTime, Random random){
                this.entityCommandBuffer = entityCommandBuffer;
                this.deltaTime = deltaTime;
                this.random = random;
            }

            public void Execute(Entity entity, int index, ref Spawner spawner, [ReadOnly] ref LocalToWorld localToWorld)
            {
                spawner.secondsToNextSpawn -= deltaTime;

                if(spawner.secondsToNextSpawn >=0) {return;}

                spawner.secondsToNextSpawn += spawner.secondsBetweenSpawns;

                Entity instance = entityCommandBuffer.Instantiate(index, spawner.prefab);
                entityCommandBuffer.SetComponent(index, instance, new Translation{
                    Value = localToWorld.Position + random.NextFloat3Direction() * random.NextFloat() * spawner.maxDistanceFromSpawner
                });
            }
        }
        
        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {
            var job = new SpawnerSystemJob(
                _endSimulationEntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent(),
                UnityEngine.Time.deltaTime,
                new Random ((uint)UnityEngine.Random.Range(0, int.MaxValue))            
            ); 
            
            
        
           JobHandle jobHandle =  job.Schedule(this, inputDependencies);
           _endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

           return jobHandle;
        }
    }
}