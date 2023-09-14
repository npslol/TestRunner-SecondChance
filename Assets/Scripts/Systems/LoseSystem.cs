using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class LoseSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<LosePanelComponent>> _losefilter = default;
        readonly EcsFilterInject<Inc<LoseEvent>> _loseEventfilter = default;

        readonly EcsPoolInject<LosePanelComponent> _losePool = default;
        readonly EcsPoolInject<LoseEvent> _loseEventPool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (int entity in _loseEventfilter.Value)
            {
                ref var loseEventComp = ref _loseEventPool.Value.Get(entity);
                GameState state = systems.GetShared<GameState>();
                state.IsLose = loseEventComp.IsLose;
                if (loseEventComp.IsLose) 
                {
                    foreach(int entity1 in _losefilter.Value)
                    {
                        ref var losePanelComp = ref _losePool.Value.Get(entity1);
                        losePanelComp.LosePanel.SetActive(true);
                        Time.timeScale = 0;
                    }

                }
            }

        }
    }
}