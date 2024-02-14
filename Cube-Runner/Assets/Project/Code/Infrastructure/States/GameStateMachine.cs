using System;
using System.Collections.Generic;
using Project.Code.Infrastructure.States.Interfaces;

namespace Project.Code.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly IGameStateFactory _gameStateFactory;

        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(IGameStateFactory gameStateFactory) =>
            _gameStateFactory = gameStateFactory;

        public void EnterState<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void EnterState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _gameStateFactory.GetState<TState>();
    }
}