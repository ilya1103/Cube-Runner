using Code.Infrastructure.States.Api;

namespace Code.Infrastructure.States
{
    public interface IGameStateMachine
    {
        void EnterState<TState>() where TState : class, IState;
        void EnterState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}