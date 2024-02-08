using System;
using Code.Infrastructure.States.Api;

namespace Code.Infrastructure.States
{
    public interface IGameStateFactory
    {
        IExitableState Create(Type type);

        T GetState<T>() where T : class, IExitableState;
    }
}