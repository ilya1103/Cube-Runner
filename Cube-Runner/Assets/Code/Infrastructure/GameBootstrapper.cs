using Code.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void Start()
        {
            //Application.targetFrameRate = 80;
            DontDestroyOnLoad(this);  
            _gameStateMachine.EnterState<BootstrapState>();
        }
    }
}