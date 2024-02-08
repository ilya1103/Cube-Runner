namespace Code.Infrastructure.States.Api
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}