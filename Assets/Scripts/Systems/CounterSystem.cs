using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class CounterSystem : IEcsRunSystem {

        readonly EcsFilterInject<Inc<CounterComponent>> _filter = default;

        readonly EcsPoolInject<CounterComponent> _pool = default;

        public void Run (IEcsSystems systems) {

                foreach (int entity in _filter.Value)
                {
                    ref var counterComp = ref _pool.Value.Get(entity);
                    GameState state = systems.GetShared<GameState>();
                    counterComp.coinText.text = state.CoinsValue.ToString();
                }
        }
    }
}