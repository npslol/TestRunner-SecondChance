using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    sealed class InitPlayer : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var go = GameObject.FindGameObjectWithTag("Player");
            var world = systems.GetWorld();
            var entity = world.NewEntity();

            ref var runnerComp = ref world.GetPool<RunnerComponent>().Add(entity);
            runnerComp.moveSpeed = 20f;
            runnerComp.minPosX = -1.35f;
            runnerComp.maxPosX = 1.4f;
            runnerComp.lastMousePos = Vector3.zero;

            ref var viewComp = ref world.GetPool<View>().Add(entity);
            viewComp.animator = go.GetComponent<Animator>();
            viewComp.collider = go.GetComponent<Collider>();
            viewComp.transform = go.transform;

            var playerMB = go.GetComponent<PlayerMonoB>();
            playerMB.World = world;
        }
    }
}
