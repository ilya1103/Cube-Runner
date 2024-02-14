using Project.Code.Infrastructure.States.Interfaces;

namespace Project.Code.Infrastructure.States
{
    public interface IGameStateMachine
    {
        void EnterState<TState>() where TState : class, IState;
        void EnterState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}