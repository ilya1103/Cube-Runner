using Project.Code.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Project.Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void Start()
        {
            Application.targetFrameRate = 70;
            _gameStateMachine.EnterState<BootstrapState>();
        }
    }
}