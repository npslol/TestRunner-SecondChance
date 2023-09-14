using UnityEngine;
using Leopotam.EcsLite;

namespace Client {
    sealed class InitWinLose : IEcsInitSystem {
        public void Init (IEcsSystems systems) {
            var winPanel = GameObject.FindGameObjectWithTag("WinPanel");
            var losePanel = GameObject.FindGameObjectWithTag("LosePanel");

            var world = systems.GetWorld();
            var entity = world.NewEntity();

            ref var winPanelComp = ref world.GetPool<WinPanelComponent>().Add(entity);
            winPanelComp.WinPanel = winPanel;
            ref var losePanelComp = ref world.GetPool<LosePanelComponent>().Add(entity);
            losePanelComp.LosePanel = losePanel;

            winPanelComp.WinPanel.SetActive(false);
            losePanelComp.LosePanel.SetActive(false);
        }
    }
}