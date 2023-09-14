using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class WinSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<WinPanelComponent>> _winfilter = default;
        readonly EcsFilterInject<Inc<WinEvent>> _winEventfilter = default;

        readonly EcsPoolInject<WinPanelComponent> _winPool = default;
        readonly EcsPoolInject<WinEvent> _winEventPool = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (int entity in _winEventfilter.Value)
            {
                ref var winEventComp = ref _winEventPool.Value.Get(entity);
                GameState state = systems.GetShared<GameState>();
                state.IsWin = winEventComp.IsWin;
                if (winEventComp.IsWin) 
                {
                    foreach(int entity1 in _winfilter.Value)
                    {
                        ref var winPanelComp = ref _winPool.Value.Get(entity1);
                        winPanelComp.WinPanel.SetActive(true);
                        Time.timeScale = 0;
                    }
                }
            }
        }
    }
}