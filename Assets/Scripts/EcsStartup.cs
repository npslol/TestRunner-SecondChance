using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        EcsSystems _systems;        

        void Start () {
            GameState state = new GameState();

            _systems = new EcsSystems (new EcsWorld(), state);
            _systems
                // register your systems here, for example:
                 
                 .Add (new InitPlayer ())
                 .Add (new InitWinLose ())
                 .Add (new InitCounter ())

                 .Add (new RunSystem ())
                 .Add (new CoinSystem())
                 .Add (new LoseSystem())
                 .Add (new WinSystem())
                 .Add (new CounterSystem())
                // .Add (new WinLoseSystem())
                // .Add (new TestSystem2 ())
                
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .DelHere<CoinEvent>()
                .DelHere<LoseEvent>()
                .DelHere<WinEvent>()

                .Inject()
                .Init ();
        }

        void Update () {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
           
        }
    }
}