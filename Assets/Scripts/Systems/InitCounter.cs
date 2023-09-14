using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

namespace Client {
    sealed class InitCounter : IEcsInitSystem {
        public void Init (IEcsSystems systems) {
            var counter = GameObject.FindGameObjectWithTag("Counter");
            var world = systems.GetWorld();
            var entity = world.NewEntity();

            ref var counterComp = ref world.GetPool<CounterComponent>().Add(entity);
            counterComp.coinText = counter.GetComponent<Text>();
        }
    }
}