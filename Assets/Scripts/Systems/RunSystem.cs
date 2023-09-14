using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class RunSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<RunnerComponent, View>> _filter = default;

        readonly EcsPoolInject<RunnerComponent> _runnerPool = default;
        readonly EcsPoolInject<View> _viewPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ref RunnerComponent runnerComp = ref _runnerPool.Value.Get(entity);
                ref View viewComp = ref _viewPool.Value.Get(entity);
                if (Input.GetMouseButtonDown(0))
                    runnerComp.lastMousePos = Input.mousePosition;
                else if (Input.GetMouseButton(0))
                {
                    viewComp.animator.SetBool("IsRunning", true);
                    Vector3 mouseDelta = Input.mousePosition - runnerComp.lastMousePos;
                    float horizontalInput = mouseDelta.x / Screen.width;
                    float newPositionX = Mathf.Clamp(
                        viewComp.transform.position.x + horizontalInput * runnerComp.moveSpeed * Time.deltaTime,
                        runnerComp.minPosX,
                        runnerComp.maxPosX
                    );
                    viewComp.transform.position = new Vector3(newPositionX, viewComp.transform.position.y, viewComp.transform.position.z);
                }
                else
                    viewComp.animator.SetBool("IsRunning", false);
            }
        }
    }
}
