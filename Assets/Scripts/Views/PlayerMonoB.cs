using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client
{
    public class PlayerMonoB : MonoBehaviour
    {
        public EcsWorld World;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                ref var eventComp = ref World.GetPool<CoinEvent>().Add(World.NewEntity());
                eventComp.CoinValue++;
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Obstacles"))
            {
                ref var loseEventComp = ref World.GetPool<LoseEvent>().Add(World.NewEntity());
                loseEventComp.IsLose = true;
            }
            else if (other.gameObject.CompareTag("WinZone"))
            {
                ref var winEventComp = ref World.GetPool<WinEvent>().Add(World.NewEntity());
                winEventComp.IsWin = true;
            }
        }
        public void SceneLoad()
        {
            SceneManager.LoadScene(0);
            World.Destroy();
            Time.timeScale = 1; 
        }
    }
}