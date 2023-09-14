using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class CoinSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<CoinEvent>> _filter = default;

        readonly EcsPoolInject<CoinEvent> _eventpool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ref var eventComp = ref _eventpool.Value.Get(entity);
                GameState state = systems.GetShared<GameState>(); 
                state.CoinsValue += eventComp.CoinValue;
            }
        }
    }
}
